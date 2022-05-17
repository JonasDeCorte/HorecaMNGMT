using Horeca.Shared.Dtos.Floorplans;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IFloorplanService
    {
        public Task<IEnumerable<FloorplanDto>> GetFloorplans();
        public Task<FloorplanDetailDto> GetFloorplanById(int id);
        public Task<HttpResponseMessage> AddFloorplan(MutateFloorplanDto dto);
        public Task<HttpResponseMessage> DeleteFloorplan(int id);
    }
}
