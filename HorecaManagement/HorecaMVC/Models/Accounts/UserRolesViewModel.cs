namespace Horeca.MVC.Models.Accounts
{
    public class UserRolesViewModel
    {
        public string Username { get; set; }
        public List<Tuple<string, string>> Permissions { get; set; }
    }
}
