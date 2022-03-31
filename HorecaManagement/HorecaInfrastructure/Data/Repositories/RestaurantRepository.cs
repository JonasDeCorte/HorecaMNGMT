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

        public Restaurant GetRestaurantIncludingDependenciesById(int restaurantId)
        {
            return context.Restaurants
                .Include(x => x.MenuCards)
                .Include(x => x.Employees)
                .ThenInclude(x => x.User)
                .Where(x => x.Id.Equals(restaurantId)).FirstOrDefault();
        }
    }
}