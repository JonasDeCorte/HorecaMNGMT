using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Accounts
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
