using Horeca.MVC.Models.Ingredients;

namespace Horeca.MVC.Models.Dishes
{
    public class DishDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string DishType { get; set; }
        public string Description { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; } = new List<IngredientViewModel>();
    }
}
