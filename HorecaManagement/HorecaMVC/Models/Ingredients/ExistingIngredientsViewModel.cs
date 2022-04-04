namespace Horeca.MVC.Models.Ingredients
{
    public class ExistingIngredientsViewModel
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; } = new List<IngredientViewModel>();
    }
}
