using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Ingredients
{
    public class ExistingIngredientsViewModel : IngredientListViewModel
    {
        [Required(ErrorMessage = "RestaurantId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "RestaurantId Id can't be 0")]
        public int RestaurantId { get; set; }

        [Required(ErrorMessage = "DishId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Dish Id can't be 0")]
        public int DishId { get; set; }

        [Required(ErrorMessage = "IngredientId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "IngredientId Id can't be 0")]
        public int IngredientId { get; set; }
    }
}