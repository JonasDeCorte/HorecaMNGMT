using Horeca.MVC.Models.Ingredients;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Ingredients
{
    public class CreateIngredientViewModel
    {
        public int IngredientId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Name { get; set; }

        [Display(Name = "Ingredient Type")]
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string IngredientType { get; set; }

        [Display(Name = "Base amount")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be higher than 0.")]
        public int BaseAmount { get; set; }

        public int UnitId { get; set; }

        public List<UnitViewModel> Units { get; set; } = new List<UnitViewModel>();
    }
}
