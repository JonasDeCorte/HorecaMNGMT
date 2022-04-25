using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Dishes
{
    public class DishViewModel
    {
        public int DishId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Category { get; set; }

        [Display(Name = "Dish Type")]
        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string DishType { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = ErrorConstants.StringLength500)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public decimal Price { get; set; }

    }
}
