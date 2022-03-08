using Horeca.Shared.Data.Entities;

namespace Horeca.MVC.Services
{
    public interface IDishService
    {
        public IEnumerable<Dish> GetDishes();
        public Dish GetDishById(int id);
        public void AddDish(Dish dish);
        public void AddDishIngredient(int id, Ingredient ingredient);
        public void DeleteDish(int id);
        public void UpdateDish(Dish dish);
    }
}
