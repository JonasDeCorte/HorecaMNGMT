using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<Restaurant> GetRestaurantIncludingDependenciesById(int restaurantId);

        Task<Restaurant> GetRestaurantIncludingMenuCardsById(int restaurantId);
    }
}