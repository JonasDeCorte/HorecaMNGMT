using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;

namespace Horeca.Infrastructure.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly DatabaseContext context;

        public OrderRepository(DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Order> CreateOrder(Receipt receipt, int tableId)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Table table = await context.Tables.FindAsync(tableId);
                Order order = null;
                if (table != null)
                {
                    order = new();
                    foreach (var receiptLine in receipt.Lines)
                    {
                        order.OrderLines.Add(new OrderLine()
                        {
                            DishId = receiptLine.Dish.Id,
                            Quantity = receiptLine.Quantity,
                            Price = receiptLine.Total,
                        });
                    }
                    order.TableId = tableId;
                    context.Orders.Add(order);
                }
                // ato dodd to kitchen
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
                return order;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
            finally
            {
                await transaction.DisposeAsync();
            }
            return null;
        }
    }
}