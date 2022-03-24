using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.UserPermissions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.UserPermissions
{
    public class AddUserPermissionsCommand : IRequest<string>
    {
        public AddUserPermissionsCommand(MutateUserPermissionsDto model)
        {
            Model = model;
        }

        public MutateUserPermissionsDto Model { get; }
    }

    public class AddUserPermissionsCommandHandler : IRequestHandler<AddUserPermissionsCommand, string>
    {
        private readonly IUnitOfWork repository;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddUserPermissionsCommandHandler(IUnitOfWork repository, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<string> Handle(AddUserPermissionsCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Model.UserName);
            if (user is null)
            {
                logger.Error(UserNotFoundException.Instance);
                throw new UserNotFoundException();
            }

            logger.Info("user needs a total of: {count} permissions", request.Model.PermissionIds.Count);

            var userperms = repository.UserPermissionRepository.GetAll().Where(x => x.UserId.Equals(user.Id)).ToList();

            logger.Info("requesting all user  permissions with a total of: {count}", userperms.Count);

            var ids = userperms.Select(x => x.PermissionId).ToList();

            var duplicates = ids.Intersect(request.Model.PermissionIds);

            foreach (var duplicateId in duplicates)
            {
                request.Model.PermissionIds.Remove(duplicateId);
            }

            foreach (var permId in request.Model.PermissionIds)
            {
                repository.UserPermissionRepository.Add(new UserPermission
                {
                    PermissionId = permId,
                    UserId = user.Id
                });
            }

            await repository.CommitAsync();

            await signInManager.SignInAsync(user, false);

            return user.Id;
        }
    }
}