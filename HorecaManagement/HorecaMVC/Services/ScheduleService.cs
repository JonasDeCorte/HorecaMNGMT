using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.RestaurantSchedules;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public ScheduleService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<RestaurantScheduleByIdDto> GetRestaurantScheduleById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{id}");
            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<RestaurantScheduleByIdDto>(await response.Content.ReadAsStringAsync());
                return result;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public async Task<IEnumerable<RestaurantScheduleDto>> GetRestaurantSchedules(int restaurantId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{ClassConstants.All}/{restaurantId}");
            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<RestaurantScheduleDto>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddRestaurantSchedule(MutateRestaurantScheduleDto restaurantScheduleDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
               $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(restaurantScheduleDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateRestaurantSchedule(MutateRestaurantScheduleDto restaurantScheduleDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                  $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(restaurantScheduleDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteRestaurantSchedule(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
               $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{id}");

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }
    }
}