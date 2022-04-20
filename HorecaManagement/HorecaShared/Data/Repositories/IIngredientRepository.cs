using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetAllIncludingUnit(int restaurantId);

        Task<Ingredient> GetIngredientIncludingUnit(int id, int restaurantId);
    }
}