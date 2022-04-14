using Horeca.Shared.Dtos.Orders;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.MVC.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<HttpResponseMessage> AddOrder(MutateOrderDto dto, int tableId);
        public Task<List<GetOrderLinesByTableIdDto>> GetOrderLinesByTableId(int tableId);
        public Task<List<OrderDtoDetail>> GetOrdersByState(int restaurantId, OrderState orderState);
        public Task<HttpResponseMessage> DeliverOrder(int restaurantId, int orderId);
        public Task<HttpResponseMessage> PrepareOrderLine(int restaurantId, int orderId, int orderLineId);
        public Task<HttpResponseMessage> ReadyOrderLine(int restaurantId, int orderId, int orderLineId);
    }
}
