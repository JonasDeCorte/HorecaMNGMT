using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        IEnumerable<Ingredient> GetAllIncludingUnit();

        Ingredient GetIngredientIncludingUnit(int id);
    }
}