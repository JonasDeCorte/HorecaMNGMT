using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Accounts
{
    public class RegisterUserViewModel : LoginUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = ErrorConstants.InvalidEmail)]
        public string Email { get; set; }
    }
}
