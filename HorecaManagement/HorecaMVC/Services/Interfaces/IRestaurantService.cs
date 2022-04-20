using Horeca.Shared.Dtos.Restaurants;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IRestaurantService
    {
        public Task<IEnumerable<RestaurantDto>> GetRestaurants();
        public Task<List<RestaurantDto>> GetRestaurantsByUser(string userId);
        public Task<DetailRestaurantDto> GetRestaurantById(int id);
        public Task<HttpResponseMessage> AddRestaurant(MutateRestaurantDto restaurantDto);
        public Task<HttpResponseMessage> UpdateRestaurant(EditRestaurantDto restaurantDto);
        public Task<HttpResponseMessage> DeleteRestaurant(int id);
        public Task<HttpResponseMessage> AddRestaurantEmployee(string userId, int restaurantId);
        public Task<HttpResponseMessage> RemoveRestaurantEmployee(string userId, int restaurantId);
        public Task<HttpResponseMessage> AddRestaurantMenuCard(int restaurantId, int menuCardId);
        public Task<HttpResponseMessage> RemoveRestaurantMenuCard(int restaurantId, int menuCardId);
        public int? GetCurrentRestaurantId();
        public string GetCurrentRestaurantName();
        public void SetCurrentRestaurant(int restaurantId, string restaurantName);
    }
}
