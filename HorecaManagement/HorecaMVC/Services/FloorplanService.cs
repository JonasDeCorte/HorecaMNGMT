using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Floorplans;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class FloorplanService : IFloorplanService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IRestaurantService restaurantService;

        public FloorplanService(HttpClient httpClient, IConfiguration configuration, IRestaurantService restaurantService)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.restaurantService = restaurantService;
        }

        public async Task<IEnumerable<FloorplanDto>> GetFloorplans()
        {
            var response = await httpClient.GetAsync($"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Floorplan}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<FloorplanDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<FloorplanDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<FloorplanDetailDto> GetFloorplanById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Floorplan}/{ClassConstants.Id}/{ClassConstants.Restaurant}" +
                $"?id={id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<FloorplanDetailDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new FloorplanDetailDto();
                }
                return result;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddFloorplan(MutateFloorplanDto dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Floorplan}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteFloorplan(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Floorplan}?id={id}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }
    }
}
