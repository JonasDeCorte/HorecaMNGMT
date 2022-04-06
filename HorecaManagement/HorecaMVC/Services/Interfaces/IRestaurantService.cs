using Horeca.Shared.Dtos.Restaurants;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IRestaurantService
    {
        public Task<IEnumerable<RestaurantDto>> GetRestaurants();
        public Task<List<RestaurantDto>> GetRestaurantsByUser(int userId);
        public Task<DetailRestaurantDto> GetRestaurantById(int id);
        public Task<HttpResponseMessage> AddRestaurant(MutateRestaurantDto restaurantDto);
        public Task<HttpResponseMessage> UpdateRestaurant(int id);
        public Task<HttpResponseMessage> DeleteRestaurant(int id);
        public Task<HttpResponseMessage> AddRestaurantEmployee(int userId, int restaurantId);
        public Task<HttpResponseMessage> RemoveRestaurantEmployee(int userId, int restaurantId);
    }
}
