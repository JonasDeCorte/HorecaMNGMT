using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        public Task<Table> GetTableById(int id, int? floorplanId);

        public Task<IEnumerable<Table>> GetAllTablesbyFloorplanId(int? floorplanId);
    }
}