namespace Horeca.MVC.Models.Accounts
{
    public class MutatePermissionsViewModel
    {
        public string Username { get; set; }

        public List<int> PermissionId { get; set; } = new List<int>();

        public List<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();
    }
}
