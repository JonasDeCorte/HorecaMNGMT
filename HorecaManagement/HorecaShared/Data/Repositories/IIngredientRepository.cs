using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;

namespace Horeca.Shared.Data.Repositories
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        IEnumerable<IngredientDto> GetAllIncludingUnit();

        IngredientDto GetIncludingUnit(int id);

        Ingredient GetIngredientIncludingUnit(int id);
    }
}