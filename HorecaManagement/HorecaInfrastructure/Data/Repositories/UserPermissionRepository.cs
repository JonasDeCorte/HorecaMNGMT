using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class UserPermissionRepository : Repository<UserPermission>, IUserPermissionRepository
    {
        private readonly DatabaseContext context;

        public UserPermissionRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async ValueTask<ClaimsIdentity?> GetUserPermissionsIdentity(string sub, CancellationToken cancellationToken)
        {
            List<Claim>? userPermissions = await context.UserPermissions
                  .Include(x => x.Permission)
                  .Include(x => x.User)
                  .Where(x => x.User.ExternalId == sub && x.IsEnabled)
                  .Select(x => new Claim(AppClaimTypes.Permissions, x.Permission.Name))
                  .ToListAsync(cancellationToken);

            return CreatePermissionsIdentity(userPermissions);
        }

        private ClaimsIdentity? CreatePermissionsIdentity(List<Claim> userPermissions)
        {
            if (!userPermissions.Any())
                return null;

            var permissionsIdentity = new ClaimsIdentity("permissions", "name", "role");
            permissionsIdentity.AddClaims(userPermissions);

            return permissionsIdentity;
        }
    }
}