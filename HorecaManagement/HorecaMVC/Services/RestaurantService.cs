using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Restaurants;
using Newtonsoft.Json;

namespace Horeca.MVC.Services
{
    public class RestaurantService : IRestaurantService
    {
        public HttpClient httpClient { get; }
        public IConfiguration configuration { get; }

        public RestaurantService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<RestaurantDto>> GetRestaurants()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}");

            var response = await httpClient.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<RestaurantDto>>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public Task<DetailRestaurantDto> GetRestaurantById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<RestaurantDto>> GetRestaurantsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AddRestaurant(MutateRestaurantDto restaurantDto)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> UpdateRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteRestaurant(int id)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> AddRestaurantEmployee(int userId, int restaurantId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> RemoveRestaurantEmployee(int userId, int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
