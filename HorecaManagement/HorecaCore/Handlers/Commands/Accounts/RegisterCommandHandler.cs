using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class RegisterCommand : IRequest<string>
    {
        public RegisterCommand(RegisterUserDto model)
        {
            Model = model;
        }

        public RegisterUserDto Model { get; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to register {object} with username: {username}", nameof(ApplicationUser), request.Model.Username);

            var userExists = await userManager.FindByNameAsync(request.Model.Username);
            if (userExists != null)
            {
                logger.Error("creating user failed, user already exists");

                throw new ArgumentException("User already exist");
            }

            ApplicationUser user = new()
            {
                Email = request.Model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Model.Username,
                ExternalId = Guid.NewGuid().ToString(),
                IsEnabled = true,
            };
            var result = await userManager.CreateAsync(user, request.Model.Password);
            logger.Info("added new user {user}", user.NormalizedUserName);
            var NewUserPerm = repository.PermissionRepository.Get(1);
            var userPerm = new UserPermission
            {
                PermissionId = NewUserPerm.Id,
                UserId = user.Id
            };
            repository.UserPermissionRepository.Add(userPerm);
            await repository.CommitAsync();
            if (!result.Succeeded)
            {
                logger.Error("creating user failed");
                throw new ArgumentNullException("Creating user failed");
            }

            return user.Id;
        }
    }
}