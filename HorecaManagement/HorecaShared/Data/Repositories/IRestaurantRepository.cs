using Horeca.Shared.Data.Entities;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.Shared.Data.Repositories
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<Restaurant> GetRestaurantByIdWithOrdersWithOrderState(int restaurantId, OrderState orderState);

        Task<Restaurant> GetRestaurantIncludingDependenciesById(int restaurantId);
    }
}