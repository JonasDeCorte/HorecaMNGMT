using Horeca.Shared.Data.Entities;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.Shared.Data.Repositories
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<Restaurant> GetRestaurantIncludingDependenciesById(int restaurantId);

        Task<Restaurant> GetRestaurantIncludingMenuCardsById(int restaurantId);

        Task<Restaurant> GetRestaurantByIdWithOrdersWithOrderState(int restaurantId, OrderState orderState);
    }
}