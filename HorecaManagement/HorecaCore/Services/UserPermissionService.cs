using Horeca.Shared.AuthUtils;
using Horeca.Shared.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Horeca.Core.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IApplicationDbContext _dbContext;

        public UserPermissionService(IApplicationDbContext dbContext)
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

            var permissionsIdentity = new ClaimsIdentity("permissions", "name", "role");
            permissionsIdentity.AddClaims(claimPermissions);

            return permissionsIdentity;
        }
    }
}