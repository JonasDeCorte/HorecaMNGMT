using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly DatabaseContext context;

        public IngredientRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Ingredient> GetAllIncludingUnit()
        {
            return context.Ingredients.Include(x => x.Unit).ToList();
        }

        public Ingredient GetIngredientIncludingUnit(int id)
        {
            return context.Ingredients.Include(x => x.Unit).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}