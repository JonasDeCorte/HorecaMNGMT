using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class DishRepository : Repository<Dish>, IDishRepository
    {
        private readonly DatabaseContext context;

        public DishRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Dish>> GetAllDishes(int restaurantId)
        {
            return await context.Dishes.Include(x => x.Restaurant)
                                 .Where(x => x.RestaurantId.Equals(restaurantId))
                                 .ToListAsync();
        }

        public async Task<Dish> GetDishById(int id, int restaurantId)
        {
            return await context.Dishes.Include(x => x.Restaurant)
                                 .Where(x => x.RestaurantId.Equals(restaurantId))
                                 .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Dish> GetDishIncludingDependencies(int id, int restaurantId)
        {
            return await context.Dishes.Include(x => x.Restaurant)
                                 .Include(x => x.DishIngredients)
                                 .ThenInclude(x => x.Ingredient)
                                 .ThenInclude(x => x.Unit)
                                 .Where(x => x.RestaurantId.Equals(restaurantId))
                                 .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}