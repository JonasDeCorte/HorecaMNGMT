using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<Menu> GetMenuIncludingDependencies(int id, int restaurantId);

        Task<Menu> GetMenuById(int id, int restaurantId);

        Task<IEnumerable<Menu>> GetAllMenus(int restaurantId);
    }
}