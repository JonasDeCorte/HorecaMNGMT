using Horeca.Shared.Data.Entities;
using System.Security.Claims;

namespace Horeca.Shared.Data.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        List<UserPermission> GetAllUserPermissionsByUserId(string userId);
    }
}