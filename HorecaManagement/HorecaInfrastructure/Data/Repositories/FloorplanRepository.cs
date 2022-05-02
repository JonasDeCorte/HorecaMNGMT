using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class FloorplanRepository : Repository<Floorplan>, IFloorplanRepository
    {
        private readonly DatabaseContext context;

        public FloorplanRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Floorplan>> GetAllFloorplans(int restaurantId)
        {
            return await context.Floorplans.Include(x => x.Restaurant)
                                 .Where(x => x.RestaurantId.Equals(restaurantId))
                                 .ToListAsync();
        }

        public Task<Floorplan> GetFloorplanById(int floorplanId)
        {
            throw new NotImplementedException();
        }
    }
}
