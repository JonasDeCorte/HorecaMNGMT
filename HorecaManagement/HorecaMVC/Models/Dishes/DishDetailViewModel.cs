using Horeca.MVC.Models.Ingredients;

namespace Horeca.MVC.Models.Dishes
{
    public class DishDetailViewModel : DishViewModel
    {
        public List<DishIngredientViewModel> Ingredients { get; set; } = new List<DishIngredientViewModel>();
    }
}
