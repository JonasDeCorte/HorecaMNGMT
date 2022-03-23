using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.UserPermissions;
using MediatR;
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
        private readonly UserManager<ApplicationUser> userManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AddUserPermissionsCommandHandler(IUnitOfWork repository, UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<string> Handle(AddUserPermissionsCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Model.UserName);
            if (user is null)
            {
                logger.Error("User not found with username: {username}", request.Model.UserName);
                throw new EntityNotFoundException("User not found");
            }

            logger.Info("user needs a total of: {count} permissions", request.Model.PermissionIds.Count);

            var userperms = repository.UserPermissionRepository.GetAll().Where(x => x.UserId.Equals(user.Id)).ToList();

            logger.Info("requesting all user  permissions with a total of: {count}", userperms.Count());

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

            logger.Info("user has  a total of: {count} permissions", user.Permissions.Count());

            return user.Id;
        }
    }
}