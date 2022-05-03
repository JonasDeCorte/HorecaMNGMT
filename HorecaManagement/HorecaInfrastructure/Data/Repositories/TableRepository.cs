using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        private readonly DatabaseContext context;

        public TableRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Table> GetTableById(int id, int? floorplanId)
        {
            return await context.Tables.Where(x => x.FloorplanId.Equals(floorplanId))
                                 .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Table>> GetAllTablesbyFloorplanId(int? floorplanId)
        {
            return await context.Tables.Where(x => x.FloorplanId.Equals(floorplanId))
                                 .ToListAsync();
        }
    }
}