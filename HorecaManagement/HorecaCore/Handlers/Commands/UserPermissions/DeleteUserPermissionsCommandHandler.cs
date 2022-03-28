using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.UserPermissions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NLog;
using System.Security.Claims;

namespace Horeca.Core.Handlers.Commands.UserPermissions
{
    public class DeleteUserPermissionsCommand : IRequest<string>
    {
        public DeleteUserPermissionsCommand(DeleteUserPermissionsDto model)
        {
            Model = model;
        }

        public DeleteUserPermissionsDto Model { get; }
    }

    public class DeleteUserPermissionsCommandHandler : IRequestHandler<DeleteUserPermissionsCommand, string>
    {
        private readonly IUnitOfWork repository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SignInManager<ApplicationUser> signInManager;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteUserPermissionsCommandHandler(IUnitOfWork repository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.signInManager = signInManager;
        }

        public async Task<string> Handle(DeleteUserPermissionsCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Model.UserName);
            if (user is null)
            {
                logger.Error(UserNotFoundException.Instance);
                throw new UserNotFoundException();
            }

            logger.Info("user wants to remove a total of: {count} permissions", request.Model.PermissionIds.Count);

            var userPermissions = repository.UserPermissions
                .GetAll()
                .Where(x => x.UserId.Equals(user.Id)).ToList();

            logger.Info("requesting all user  permissions with a total of: {count}", userPermissions.Count);

            List<UserPermission> userpermToDelete = userPermissions.SelectMany(
                userperm => request.Model.PermissionIds
                .Where(modelPermId =>
                userperm.PermissionId.Equals(modelPermId))
                .Select(modelPermId => userperm))
                .ToList();

            logger.Info("checking which permissions to remove with a total of: {count}", userpermToDelete.Count);

            var claimsIdentity = RemoveClaims(userpermToDelete);

            foreach (var userperm in userpermToDelete)
            {
                repository.UserPermissions.Delete(userperm.Id);
                logger.Info("removed permission : {object}", userperm.Id);
            }

            await repository.CommitAsync();

            await signInManager.SignInAsync(user, false);
            logger.Info("refresh user sign in : {object}", user.Id);

            return user.Id;
        }

        private ClaimsIdentity RemoveClaims(List<UserPermission> duplicates)
        {
            ClaimsIdentity? identity = httpContextAccessor.HttpContext.User.Identities.Last();

            var userClaims = identity.Claims.ToList();
            foreach (Claim? claim in userClaims)
            {
                identity = CompareClaimsToRemove(duplicates, identity, claim);
            }
            return identity;
        }

        private ClaimsIdentity CompareClaimsToRemove(List<UserPermission> duplicates, ClaimsIdentity identity, Claim claim)
        {
            for (int j = 0; j < duplicates.Count; j++)
            {
                Permission? permission = repository.PermissionRepository.Get(duplicates[j].PermissionId);

                if (claim.Value.Equals(permission.Name, StringComparison.Ordinal))
                {
                    identity.RemoveClaim(claim);
                }
            }
            return identity;
        }
    }
}