using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
        Task<Dish> GetDishIncludingDependencies(int id, int restaurantId);

        Task<Dish> GetDishIncludingIngredient(int id, int restaurantId);

        Task<IEnumerable<Dish>> GetAllDishes(int restaurantId);

        Task<Dish> GetDishById(int id, int restaurantId);
    }
}