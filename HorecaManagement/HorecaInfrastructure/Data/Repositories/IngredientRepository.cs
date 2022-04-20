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

        public async Task<IEnumerable<Ingredient>> GetAllIncludingUnit(int restaurantId)
        {
            return await context.Ingredients.Include(x => x.Unit)
                                            .Include(x => x.Restaurant)
                                            .Where(x => x.RestaurantId.Equals(restaurantId))
                                            .ToListAsync();
        }

        public async Task<Ingredient> GetIngredientIncludingUnit(int id, int restaurantId)
        {
            return await context.Ingredients.Include(x => x.Unit)
                                            .Include(x => x.Restaurant)
                                            .Where(x => x.RestaurantId.Equals(restaurantId))
                                            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}