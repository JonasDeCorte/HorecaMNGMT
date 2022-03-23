using Horeca.Shared.Data.Entities;
using System.Security.Claims;

namespace Horeca.Shared.Data.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        public ValueTask<ClaimsIdentity?> GetUserPermissionsIdentity(string sub, CancellationToken cancellationToken);
    }
}