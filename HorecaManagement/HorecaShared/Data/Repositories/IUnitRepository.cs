using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetAllUnits(int restaurantId);

        Task<Unit> GetUnitById(int id, int restaurantId);
    }
}