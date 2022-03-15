using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IDishService
    {
        public Task<IEnumerable<DishDto>> GetDishes();
        public Task<DishDto> GetDishById(int id);
        public Task<DishIngredientsByIdDto> GetIngredientsByDishId(int id);
        public Task<Dish> GetDishDetailById(int id);
        public void AddDish(MutateDishDto dish);
        public void AddDishIngredient(int id, MutateIngredientByDishDto ingredient);
        public void DeleteDish(int id);
        public void DeleteDishIngredient(DeleteIngredientDishDto ingredient);
        public void UpdateDish(MutateDishDto dish);
        public void UpdateDishIngredient(MutateIngredientByDishDto ingredient);
    }
}
