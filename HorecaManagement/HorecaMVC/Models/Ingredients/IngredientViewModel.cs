using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Ingredients
{
    public class IngredientViewModel
    {
        public int IngredientId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Name { get; set; }

        [Display(Name = "Ingredient Type")]
        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string IngredientType { get; set; }

        [Display(Name = "Base amount")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public int BaseAmount { get; set; }

        [Display(Name = "Unit name")]
        [Required]
        public UnitViewModel Unit { get; set; }
    }
}
