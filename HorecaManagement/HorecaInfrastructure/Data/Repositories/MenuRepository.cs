using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly DatabaseContext context;

        public MenuRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllMenus(int restaurantId)
        {
            return await context.Menus.Include(x => x.Restaurant)
                                       .Where(x => x.RestaurantId.Equals(restaurantId))
                                       .ToListAsync();
        }

        public async Task<Menu> GetMenuById(int id, int restaurantId)
        {
            return await context.Menus.Include(x => x.Restaurant)
                                      .Where(x => x.RestaurantId.Equals(restaurantId))
                                      .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<Menu> GetMenuIncludingDependencies(int id, int restaurantId)
        {
            return await context.Menus.Include(x => x.Dishes)
                                      .Where(x => x.RestaurantId.Equals(restaurantId))
                                      .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}