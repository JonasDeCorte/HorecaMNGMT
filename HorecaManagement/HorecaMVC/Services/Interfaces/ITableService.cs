using Horeca.Shared.Dtos.Floorplans;
using Horeca.Shared.Dtos.Tables;

namespace Horeca.MVC.Services.Interfaces
{
    public interface ITableService
    {
        public Task<IEnumerable<TableDto>> GetTables(int floorplanId);
        public Task<TableDto> GetTableById(int tableId, int floorplanId);
        public Task<HttpResponseMessage> AddTablesFromFloorplan(FloorplanDetailDto dto);
    }
}
