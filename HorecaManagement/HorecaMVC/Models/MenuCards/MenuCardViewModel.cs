using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.MenuCards
{
    public class MenuCardViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Name { get; set; }
    }
}
