using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class MenuCardRepository : Repository<MenuCard>, IMenuCardRepository
    {
        private readonly DatabaseContext context;

        public MenuCardRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<MenuCard>> GetAllMenuCards(int restaurantId)
        {
            return await context.MenuCards
                                         .Where(x => x.RestaurantId.Equals(restaurantId))
                                         .ToListAsync();
        }

        public async Task<MenuCard> GetMenuCardById(int id, int restaurantId)
        {
            return await context.MenuCards
                                         .Where(x => x.RestaurantId.Equals(restaurantId))
                                         .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<MenuCard> GetMenuCardIncludingDependencies(int id, int restaurantId)
        {
            return await context.MenuCards.Include(x => x.Menus)
                                          .Include(x => x.Dishes)
                                          .Where(x => x.RestaurantId.Equals(restaurantId))
                                          .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<MenuCard> GetMenuCardIncludingDishes(int id, int restaurantId)
        {
            return await context.MenuCards
                                         .Include(x => x.Dishes)
                                         .Where(x => x.RestaurantId.Equals(restaurantId))
                                         .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<MenuCard> GetMenuCardIncludingMenus(int id, int restaurantId)
        {
            return await context.MenuCards.Include(x => x.Menus)
                                        .Where(x => x.RestaurantId.Equals(restaurantId))
                                        .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}