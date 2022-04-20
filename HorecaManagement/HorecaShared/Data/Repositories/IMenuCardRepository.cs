using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IMenuCardRepository : IRepository<MenuCard>
    {
        Task<MenuCard> GetMenuCardIncludingDependencies(int id, int restaurantId);

        Task<MenuCard> GetMenuCardIncludingDishes(int id, int restaurantId);

        Task<MenuCard> GetMenuCardIncludingMenus(int id, int restaurantId);

        Task<MenuCard> GetMenuCardById(int id, int restaurantId);

        Task<IEnumerable<MenuCard>> GetAllMenuCards(int restaurantId);
    }
}