using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IDishService
    {
        public IEnumerable<Dish> GetDishes();
        public Dish GetDishById(int id);
        public DishIngredientsByIdDto GetDishIngredientsById(int id);
        public void AddDish(Dish dish);
        public void AddDishIngredient(int id, MutateIngredientByDishDto ingredient);
        public void DeleteDish(int id);
        public void DeleteDishIngredient(DeleteIngredientDishDto ingredient);
        public void UpdateDish(Dish dish);
        public void UpdateDishIngredient(MutateIngredientByDishDto ingredient);
    }
}
