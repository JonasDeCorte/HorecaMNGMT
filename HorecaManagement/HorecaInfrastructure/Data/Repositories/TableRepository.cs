using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class TableRepository : Repository<Table>, ITableRepository
    {
        public TableRepository(DatabaseContext context) : base(context)
        {
        }
    }
}