using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Accounts
{
    public class LoginUserViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
