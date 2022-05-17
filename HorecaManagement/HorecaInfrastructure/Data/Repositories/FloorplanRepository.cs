using Horeca.Core.Exceptions;
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

        public async Task<Floorplan> GetFloorplanById(int id, int restaurantId)
        {
            return await context.Floorplans.Include(x => x.Restaurant)
                                 .Where(x => x.RestaurantId.Equals(restaurantId))
                                 .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<int> DeleteFloorplan(int id)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = context.Database.BeginTransaction();
            try
            {
                var floorplan = await context.Floorplans.FindAsync(id);
                if (floorplan != null)
                {
                    var tables = context.Tables.Where(x => x.FloorplanId.Equals(id));
                    foreach (var table in tables)
                    {
                        context.Tables.Remove(table);
                    }
                    context.Floorplans.Remove(floorplan);
                }
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return 0;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }
    }
}