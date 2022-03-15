using Horeca.Shared.Dtos.Units;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Ingredients
{
    public class MutateIngredientViewModel
    {
        public int DishId { get; set; }
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters!")]
        public string IngredientType { get; set; }
        [Display(Name = "Base amount")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "{0} may not be a negative value or 0.")]
        public int BaseAmount { get; set; }
        [Display(Name = "Unit name")]
        [Required(ErrorMessage = "Unit name is required.")]
        public UnitDto Unit { get; set; }
    }
}
