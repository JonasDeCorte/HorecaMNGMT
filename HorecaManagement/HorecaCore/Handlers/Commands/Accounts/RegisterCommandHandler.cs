using Horeca.Core.Exceptions;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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
                logger.Error(RegisterException.Instance);
                throw new RegisterException();
            }

            ApplicationUser user = new()
            {
                Email = request.Model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Model.Username,
                ExternalId = Guid.NewGuid().ToString(),
                IsEnabled = true,
                IsOwner = request.Model.IsOwner,
            };

            var result = await userManager.CreateAsync(user, request.Model.Password);
            if (result.Succeeded)
            {
                logger.Info("added new user {user}", user.NormalizedUserName);

                UserPermission userPerm = AddNewUserPermission(user);

                IsOwnerPermissions(user);

                await repository.CommitAsync();

                logger.Info("added default permission new user {userperm}", userPerm);
            }

            if (!result.Succeeded)
            {
                logger.Error(RegisterException.Instance);
                throw new RegisterException();
            }

            return user.Id;
        }

        private UserPermission AddNewUserPermission(ApplicationUser user)
        {
            var NewUserPerm = repository.PermissionRepository.Get(1);

            var userPerm = new UserPermission
            {
                PermissionId = NewUserPerm.Id,
                UserId = user.Id
            };

            repository.UserPermissions.Add(userPerm);
            return userPerm;
        }

        private void IsOwnerPermissions(ApplicationUser user)
        {
            if (user.IsOwner)
            {
                foreach (var perm in repository.PermissionRepository.GetAll().Skip(1))
                {
                    var Userpermission = new UserPermission
                    {
                        PermissionId = perm.Id,
                        UserId = user.Id
                    };
                    repository.UserPermissions.Add(Userpermission);
                }
            }
        }
    }
}