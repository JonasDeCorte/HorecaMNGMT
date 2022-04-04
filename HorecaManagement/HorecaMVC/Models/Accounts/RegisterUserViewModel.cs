using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Accounts
{
    public class RegisterUserViewModel : LoginUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
