namespace Horeca.MVC.Models.Ingredients
{
    public class ExistingIngredientsViewModel : IngredientListViewModel
    {
        public int RestaurantId { get; set; }

        public int DishId { get; set; }

        public int IngredientId { get; set; }
    }
}
