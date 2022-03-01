using Horeca.Infrastructure.Data.Repositories.Generic;
using HorecaAPI.Data.Entities;
using HorecaAPI.Data.Repositories;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(DatabaseContext context) : base(context)
        {
        }
    }
}