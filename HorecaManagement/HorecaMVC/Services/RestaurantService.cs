using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Dtos.Restaurants;

namespace Horeca.MVC.Services
{
    public class RestaurantService : IRestaurantService
    {
        public Task<DetailRestaurantDto> GetRestaurantById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RestaurantDto>> GetRestaurants()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AddRestaurant(MutateRestaurantDto restaurantDto)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AddRestaurantEmployee(int userId, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> RemoveRestaurantEmployee(int userId, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UpdateRestaurant(int id)
        {
            throw new NotImplementedException();
        }
    }
}
