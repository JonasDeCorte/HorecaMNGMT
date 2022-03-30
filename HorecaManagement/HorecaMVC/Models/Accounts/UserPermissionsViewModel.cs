using Horeca.Shared.Dtos.Accounts;

namespace Horeca.MVC.Models.Accounts
{
    public class UserPermissionsViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}
