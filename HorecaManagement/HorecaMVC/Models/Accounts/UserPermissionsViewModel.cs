namespace Horeca.MVC.Models.Accounts
{
    public class UserPermissionsViewModel
    {
        public string Username { get; set; }

        public List<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();
    }
}
