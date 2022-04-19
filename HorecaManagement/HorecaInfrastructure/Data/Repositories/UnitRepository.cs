using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly DatabaseContext context;

        public UnitRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Unit>> GetAllUnits(int restaurantId)
        {
            return await context.Units.Include(x => x.Restaurant)
                                      .Where(x => x.RestaurantId.Equals(restaurantId))
                                      .ToListAsync();
        }

        public async Task<Unit> GetUnitById(int id, int restaurantId)
        {
            return await context.Units.Include(x => x.Restaurant)
                                     .Where(x => x.RestaurantId.Equals(restaurantId))
                                     .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}