using Horeca.Shared.Dtos.Accounts;

namespace Horeca.MVC.Models.Accounts
{
    public class UserRolesViewModel
    {
        public string Username { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }
}
