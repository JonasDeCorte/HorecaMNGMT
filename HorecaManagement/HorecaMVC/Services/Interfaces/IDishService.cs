using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IDishService
    {
        public Task<IEnumerable<DishDto>> GetDishes(string token);
        public Task<DishDto> GetDishById(int id, string token);
        public Task<DishIngredientsByIdDto> GetIngredientsByDishId(int id, string token);
        public Task<Dish> GetDishDetailById(int id, string token);
        public Task<HttpResponseMessage> AddDish(MutateDishDto dish, string token);
        public Task<HttpResponseMessage> AddDishIngredient(int id, MutateIngredientByDishDto ingredient, string token);
        public Task<HttpResponseMessage> DeleteDish(int id, string token);
        public Task<HttpResponseMessage> DeleteDishIngredient(DeleteIngredientDishDto ingredient, string token);
        public Task<HttpResponseMessage> UpdateDish(MutateDishDto dish, string token);
        public Task<HttpResponseMessage> UpdateDishIngredient(MutateIngredientByDishDto ingredient, string token);
    }
}
