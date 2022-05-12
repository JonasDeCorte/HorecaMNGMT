using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Floorplans;
using Horeca.Shared.Dtos.Tables;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class TableService : ITableService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IRestaurantService restaurantService;

        public TableService(HttpClient httpClient, IConfiguration configuration, IRestaurantService restaurantService)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.restaurantService = restaurantService;
        }

        public async Task<TableDto> GetTableById(int tableId, int floorplanId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Table}/{ClassConstants.Floorplan}" +
                $"?id={tableId}&{ClassConstants.FloorplanId}={floorplanId}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<TableDto>(await response.Content.ReadAsStringAsync());
                if (result == null)
                {
                    return new TableDto();
                }
                return result;
            }
            return null;
        }

        public Task<IEnumerable<TableDto>> GetTables(int floorplanId)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> AddTablesFromFloorplan(FloorplanDetailDto dto, int floorplanId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Table}/{ClassConstants.Floorplan}" +
                $"?{ClassConstants.FloorplanId}={floorplanId}&{ClassConstants.RestaurantId}={restaurantService.GetCurrentRestaurantId()}")
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

        public async Task<HttpResponseMessage> DeleteTable(int tableId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Table}?id={tableId}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }
    }
}
