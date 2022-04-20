using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Accounts
{
    public class LoginUserViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
