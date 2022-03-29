using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IDishService
    {
        public Task<IEnumerable<DishDto>> GetDishes(string token);
        public Task<DishDto> GetDishById(int id);
        public Task<DishIngredientsByIdDto> GetIngredientsByDishId(int id);
        public Task<Dish> GetDishDetailById(int id);
        public Task<HttpResponseMessage> AddDish(MutateDishDto dish);
        public Task<HttpResponseMessage> AddDishIngredient(int id, MutateIngredientByDishDto ingredient);
        public Task<HttpResponseMessage> DeleteDish(int id);
        public Task<HttpResponseMessage> DeleteDishIngredient(DeleteIngredientDishDto ingredient);
        public Task<HttpResponseMessage> UpdateDish(MutateDishDto dish);
        public Task<HttpResponseMessage> UpdateDishIngredient(MutateIngredientByDishDto ingredient);
    }
}
