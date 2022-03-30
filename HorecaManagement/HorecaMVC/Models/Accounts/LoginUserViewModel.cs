using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Accounts
{
    public class LoginUserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
