using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using static Horeca.Shared.Utils.Constants;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                //  fout in design?
                Table table = await context.Tables
                    .Include(x => x.BookingDetail)
                    .ThenInclude(x => x.RestaurantSchedule)
                    .ThenInclude(x => x.Restaurant)
                    .ThenInclude(x => x.Kitchen)
                    .ThenInclude(x => x.Orders)
                    .SingleOrDefaultAsync(x => x.Id.Equals(tableId));

                Order order = null;

                if (table != null)
                {
                    order = new();

                    AddOrderLines(receipt, order);

                    order.OrderState = OrderState.Confirmed;
                    table.Orders.Add(order);

                    context.Orders.Add(order);
                    context.Tables.Update(table);
                }

                // notifies kitchen an order has been placed
                var kitchen = table.RestaurantSchedule.Restaurant.Kitchen;
                kitchen.Orders.Add(order);
                context.Kitchens.Update(kitchen);

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

        private static void AddOrderLines(Receipt receipt, Order order)
        {
            order.OrderLines.AddRange(receipt.Lines.Select(receiptLine => new OrderLine()
            {
                DishId = receiptLine.Dish.Id,
                Quantity = receiptLine.Quantity,
                Price = receiptLine.Total,
                DishState = DishState.Waiting
            }));
        }
    }
}