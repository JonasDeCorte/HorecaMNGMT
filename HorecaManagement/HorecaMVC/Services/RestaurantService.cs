using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Restaurants;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RestaurantService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<RestaurantDto>> GetRestaurants()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<RestaurantDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<RestaurantDto>();
                }
                SetCurrentRestaurant(0, "Horeca");
                return result;
            }
            return null;
        }

        public async Task<DetailRestaurantDto> GetRestaurantById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}/{ClassConstants.Id}?id={id}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<DetailRestaurantDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new DetailRestaurantDto();
                }
                SetCurrentRestaurant(id, result.Name);
                return result;
            }
            return null;
        }

        public async Task<List<RestaurantDto>> GetRestaurantsByUser(string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}/{ClassConstants.User}?userId={userId}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<RestaurantDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<RestaurantDto>();
                }
                SetCurrentRestaurant(0, "Horeca");
                return result;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddRestaurant(MutateRestaurantDto restaurantDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(restaurantDto), Encoding.UTF8, "application/json")
            };
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateRestaurant(EditRestaurantDto restaurantDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(restaurantDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteRestaurant(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}/{ClassConstants.Id}?id={id}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddRestaurantEmployee(string employeeId, int restaurantId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}/{ClassConstants.Employee}" +
                $"?{ClassConstants.RestaurantId}={restaurantId}&{ClassConstants.EmployeeId}={employeeId}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> RemoveRestaurantEmployee(string employeeId, int restaurantId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}/{ClassConstants.Employee}" +
                $"?{ClassConstants.RestaurantId}={restaurantId}&{ClassConstants.EmployeeId}={employeeId}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddRestaurantMenuCard(int restaurantId, int menuCardId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}/{restaurantId}/" +
                $"{ClassConstants.MenuCard}/{menuCardId}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> RemoveRestaurantMenuCard(int restaurantId, int menuCardId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Restaurant}/{restaurantId}/" +
                $"{ClassConstants.MenuCard}/{menuCardId}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public int? GetCurrentRestaurantId()
        {
            return httpContextAccessor.HttpContext.Session.GetInt32("CurrentRestaurantId");
        }

        public string GetCurrentRestaurantName()
        {
            return httpContextAccessor.HttpContext.Session.GetString("CurrentRestaurantName");
        }

        public void SetCurrentRestaurant(int restaurantId, string restaurantName)
        {
            var currentId = httpContextAccessor.HttpContext.Session.GetInt32("CurrentRestaurantId");
            if (currentId != null)
            {
                httpContextAccessor.HttpContext.Session.Remove("CurrentRestaurantId");
                httpContextAccessor.HttpContext.Session.Remove("CurrentRestaurantName");
            }
            httpContextAccessor.HttpContext.Session.SetInt32("CurrentRestaurantId", restaurantId);
            httpContextAccessor.HttpContext.Session.SetString("CurrentRestaurantName", restaurantName);
        }
    }
}