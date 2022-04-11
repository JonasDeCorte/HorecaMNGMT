using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Task<Order> CreateOrder(Receipt receipt, int tableId);

        public Task<List<Order>> GetOrdersByTable(int tableId);
    }
}