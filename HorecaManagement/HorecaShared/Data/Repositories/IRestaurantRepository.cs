using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Restaurant GetRestaurantIncludingDependenciesById(int restaurantId);
    }
}