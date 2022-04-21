using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Menus
{
    public class MenuViewModel
    {
        public int MenuId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Category { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = ErrorConstants.StringLength50)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public decimal Price { get; set; }
    }
}
