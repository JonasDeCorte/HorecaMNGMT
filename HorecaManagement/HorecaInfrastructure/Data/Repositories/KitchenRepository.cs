using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class KitchenRepository : Repository<Kitchen>, IKitchenRepository
    {
        private readonly DatabaseContext context;

        public KitchenRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Kitchen> GetKitchenWithDependenciesByID(int kitchenId)
        {
            return await context.Kitchens.Include(x => x.Orders).ThenInclude(x => x.OrderLines).SingleOrDefaultAsync(x => x.Id.Equals(kitchenId));
        }
    }
}