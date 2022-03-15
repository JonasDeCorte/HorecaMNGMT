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

        public Table GetTableIncludingDependencies(int id)
        {
            return context.Tables.Include(x => x.Reservation).Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        public List<Table> GetTablesIncludingDependencies()
        {
            return context.Tables.Include(x => x.Reservation).ToList();
        }
    }
}