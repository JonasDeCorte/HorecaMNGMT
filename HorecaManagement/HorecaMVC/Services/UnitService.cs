using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Units;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class UnitService : IUnitService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IRestaurantService restaurantService;

        public UnitService(HttpClient httpClient, IConfiguration configuration, IRestaurantService restaurantService)
        {
            this.configuration = configuration;
            this.restaurantService = restaurantService;
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<UnitDto>> GetUnits()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Unit}/{ClassConstants.All}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<UnitDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<UnitDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<UnitDto> GetUnitById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Unit}/" +
                $"{ClassConstants.Restaurant}?id={id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<UnitDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new UnitDto();
                }
                return result;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddUnit(MutateUnitDto unitDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Unit}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(unitDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateUnit(MutateUnitDto unitDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Unit}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}&id={unitDto.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(unitDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteUnit(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Unit}?id={id}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }
    }
}