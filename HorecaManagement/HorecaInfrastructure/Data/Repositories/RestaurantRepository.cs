using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        private readonly DatabaseContext context;

        public RestaurantRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Restaurant> GetRestaurantIncludingDependenciesById(int restaurantId)
        {
            return await context.Restaurants
                .Include(x => x.MenuCards)
                .Include(x => x.Employees)
                .ThenInclude(x => x.User)
                .Where(x => x.Id.Equals(restaurantId)).FirstOrDefaultAsync();
        }

        public async Task<Restaurant> GetRestaurantIncludingMenuCardsById(int restaurantId)
        {
            return await context.Restaurants
                .Include(x => x.MenuCards)
                .Where(x => x.Id.Equals(restaurantId)).FirstOrDefaultAsync();
        }
    }
}