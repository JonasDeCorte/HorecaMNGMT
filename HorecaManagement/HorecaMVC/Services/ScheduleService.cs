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

        public ScheduleService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<ScheduleByIdDto> GetScheduleById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{id}");
            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<ScheduleByIdDto>(await response.Content.ReadAsStringAsync());
                return result;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedules(int restaurantId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}/{ClassConstants.All}/{restaurantId}");
            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<ScheduleDto>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null;
            }
            return null;
        }

        public async Task<HttpResponseMessage> AddSchedule(MutateScheduleDto scheduleDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
               $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(scheduleDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> UpdateSchedule(MutateScheduleDto scheduleDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                  $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Schedule}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(scheduleDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return response;
        }

        public async Task<HttpResponseMessage> DeleteSchedule(int id)
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