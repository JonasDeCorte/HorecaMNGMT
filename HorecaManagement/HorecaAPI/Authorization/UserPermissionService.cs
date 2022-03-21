using Horeca.Infrastructure.Data;
using Horeca.Shared.AuthUtils;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Horeca.API.Authorization
{
    public interface IUserPermissionService
    {
        /// <summary>
        /// Returns a new identity containing the user permissions as Claims
        /// </summary>
        /// <param name="sub">The user external id (sub claim)</param>
        /// <param name="cancellationToken"></param>
        ValueTask<ClaimsIdentity?> GetUserPermissionsIdentity(string sub, CancellationToken cancellationToken);
    }

    public class UserPermissionService : IUserPermissionService
    {
        private readonly DatabaseContext _dbContext;

        public UserPermissionService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<ClaimsIdentity?> GetUserPermissionsIdentity(string sub, CancellationToken cancellationToken)
        {
            var userPermissions = await _dbContext.UserPermissions
                .Include(x => x.Permission)
                .Include(x => x.User)
                .Where(x => x.User.ExternalId == sub)
                .Select(x => new Claim(AppClaimTypes.Permissions, x.Permission.Name))
                .ToListAsync(cancellationToken);

            return CreatePermissionsIdentity(userPermissions);
        }

        private static ClaimsIdentity? CreatePermissionsIdentity(IReadOnlyCollection<Claim> claimPermissions)
        {
            if (!claimPermissions.Any())
                return null;

            var permissionsIdentity = new ClaimsIdentity(nameof(PermissionsMiddleware), "name", "role");
            permissionsIdentity.AddClaims(claimPermissions);

            return permissionsIdentity;
        }
    }
}