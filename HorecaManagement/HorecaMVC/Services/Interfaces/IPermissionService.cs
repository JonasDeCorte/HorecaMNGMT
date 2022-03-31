using Horeca.Shared.Dtos.Accounts;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IPermissionService
    {
        public Task<List<PermissionDto>> GetPermissions();
        public Task<PermissionDto> GetPermissionById(int id);
    }
}
