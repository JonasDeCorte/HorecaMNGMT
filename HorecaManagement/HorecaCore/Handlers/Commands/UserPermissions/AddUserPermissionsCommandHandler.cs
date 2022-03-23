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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AddUserPermissionsCommandHandler(IUnitOfWork repository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
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

            var permissionsIdentity = await repository.UserPermissionRepository.GetUserPermissionsIdentity(user.ExternalId, cancellationToken);
            logger.Info("requesting permission identity to add to the user {@object}", permissionsIdentity);

            httpContextAccessor.HttpContext.User.AddIdentity(permissionsIdentity);

            return user.Id;
        }
    }
}