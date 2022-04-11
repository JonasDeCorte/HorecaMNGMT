using Horeca.Core.Exceptions;
using Horeca.Infrastructure.Data.Repositories.Generic;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using static Horeca.Shared.Utils.Constants;

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
                Table table = await context.Tables.SingleOrDefaultAsync(x => x.Id.Equals(tableId));

                if (table == null)
                {
                    throw new EntityNotFoundException();
                }
                var restaurantSchedule = await context.RestaurantSchedules.SingleOrDefaultAsync(x => x.Id == table.RestaurantScheduleId);

                if (restaurantSchedule == null)
                {
                    throw new EntityNotFoundException();
                }
                var restaurant = await context.Restaurants.SingleOrDefaultAsync(x => x.Id == restaurantSchedule.RestaurantId);

                if (restaurant == null)
                {
                    throw new EntityNotFoundException();
                }
                Order order = new();

                AddOrderLines(receipt, order);

                order.OrderState = OrderState.Begin;
                table.Orders.Add(order);
                restaurant.Orders.Add(order);

                context.Orders.Add(order);

                context.Tables.Update(table);
                context.Restaurants.Update(restaurant);

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

        public async Task<List<Order>> GetOrdersByTable(int tableId)
        {
            return await context.Orders.Include(x => x.OrderLines)
                                       .ThenInclude(x => x.Dish)
                                       .Where(x => x.TableId.Equals(tableId))
                                       .ToListAsync();
        }
    }
}