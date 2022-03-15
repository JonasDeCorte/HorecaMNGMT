using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        List<Table> GetTablesIncludingDependencies();

        Table GetTableIncludingDependencies(int id);
    }
}