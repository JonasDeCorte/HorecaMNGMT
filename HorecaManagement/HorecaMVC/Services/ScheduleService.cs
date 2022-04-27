using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Schedules;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IRestaurantService restaurantService;

        public ScheduleService(HttpClient httpClient, IConfiguration configuration, IRestaurantService restaurantService)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.restaurantService = restaurantService;
        }

        public async Task<ScheduleByIdDto> GetScheduleById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{ClassConstants.Restaurant}" +
                $"?id={id}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ScheduleByIdDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new ScheduleByIdDto();
                }
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedules(int restaurantId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{ClassConstants.All}/{ClassConstants.Restaurant}" +
                $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}");
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<ScheduleDto>>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new List<ScheduleDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddSchedule(MutateScheduleDto scheduleDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
               $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{ClassConstants.Restaurant}" +
               $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(scheduleDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> UpdateSchedule(MutateScheduleDto scheduleDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                  $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{ClassConstants.Restaurant}" +
               $"?{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}&id={scheduleDto.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(scheduleDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeleteSchedule(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
               $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{ClassConstants.Restaurant}" +
               $"?id={restaurantService.GetCurrentRestaurantId()}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }
    }
}