using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Ingredients
{
    public class ExistingIngredientsViewModel : IngredientListViewModel
    {
       
        public int RestaurantId { get; set; }

        public int DishId { get; set; }

        [Required(ErrorMessage = "IngredientId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "IngredientId Id can't be 0")]
        public int IngredientId { get; set; }
    }
}