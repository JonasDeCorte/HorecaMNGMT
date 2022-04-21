using Horeca.MVC.Services.Interfaces;
using Horeca.Shared.Constants;
using Horeca.Shared.Dtos.Orders;
using Horeca.Shared.Utils;
using Newtonsoft.Json;
using System.Text;

namespace Horeca.MVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<HttpResponseMessage> AddOrder(MutateOrderDto orderDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{configuration.GetSection("BaseURL").Value}/" +
                $"{ClassConstants.Order}/{ClassConstants.Table}/{orderDto.TableId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(orderDto), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }


        public async Task<List<GetOrderLinesByTableIdDto>> GetOrderLinesByTableId(int tableId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Order}/{ClassConstants.Table}/{tableId}/{ClassConstants.Details}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<GetOrderLinesByTableIdDto>>(response.Content.ReadAsStringAsync().Result);
                if (result == null)
                {
                    return new List<GetOrderLinesByTableIdDto>();
                }
                return result;
            }
            return null;
        }

        public async Task<List<OrderDtoDetail>> GetOrdersByState(int restaurantId, Constants.OrderState orderState)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Order}/{ClassConstants.Restaurant}/{restaurantId}/" +
                $"{ClassConstants.Orders}/{orderState}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<OrderDtoDetail>>(response.Content.ReadAsStringAsync().Result);
                if (result == null)
                {
                    return new List<OrderDtoDetail>();
                }
                return result;
            }
            return null;
        }

        public async Task<HttpResponseMessage> DeliverOrder(int restaurantId, int orderId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Order}/{ClassConstants.Restaurant}/{restaurantId}/" +
                $"{ClassConstants.Order}/{orderId}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> PrepareOrderLine(int restaurantId, int orderId, int orderLineId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Order}/{ClassConstants.Restaurant}/{restaurantId}/" +
                $"{ClassConstants.Order}/{orderId}/{ClassConstants.OrderLine}/{orderLineId}/{ClassConstants.Prepare}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }

        public async Task<HttpResponseMessage> ReadyOrderLine(int restaurantId, int orderId, int orderLineId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                $"{configuration.GetSection("BaseURL").Value}/{ClassConstants.Order}/{ClassConstants.Restaurant}/{restaurantId}/" +
                $"{ClassConstants.Order}/{orderId}/{ClassConstants.OrderLine}/{orderLineId}/{ClassConstants.Ready}");

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            return null;
        }
    }
}
