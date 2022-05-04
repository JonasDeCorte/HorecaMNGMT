using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IFloorplanRepository : IRepository<Floorplan>
    {
        public Task<Floorplan> GetFloorplanById(int id, int restaurantId);

        public Task<IEnumerable<Floorplan>> GetAllFloorplans(int restaurantId);
    }
}
