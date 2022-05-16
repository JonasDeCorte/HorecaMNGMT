using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Orders;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Orders;

namespace Horeca.MVC.Helpers.Mappers
{
    public static class OrderMapper
    {
        public static OrderListViewModel MapOrderListModel(List<OrderDtoDetail> orders, int restaurantId)
        {
            OrderListViewModel listModel = new OrderListViewModel()
            {
                RestaurantId = restaurantId,
            };
            foreach (var order in orders)
            {
                OrderViewModel orderModel = MapOrderModel(order);
                listModel.Orders.Add(orderModel);
            }
            return listModel;
        }

        public static OrderViewModel MapOrderModel(OrderDtoDetail order)
        {
            OrderViewModel model = new OrderViewModel()
            {
                Id = order.Id,
                TableId = order.TableId,
                OrderState = order.OrderState
            };
            foreach (var orderLine in order.Lines)
            {
                OrderLineViewModel lineModel = MapOrderLineModel(orderLine);
                model.Lines.Add(lineModel);
            }
            return model;
        }

        public static OrderViewModel MapOrderModel(GetOrderLinesByTableIdDto order)
        {
            OrderViewModel model = new OrderViewModel()
            {
                Id = order.Id,
                TableId = order.TableId,
                OrderState = order.OrderState
            };
            foreach (var orderLine in order.Lines)
            {
                OrderLineViewModel lineModel = MapOrderLineModel(orderLine);
                model.Lines.Add(lineModel);
            }
            return model;
        }

        public static OrderLineViewModel MapOrderLineModel(OrderLineDto orderLine)
        {
            return new OrderLineViewModel()
            {
                Id = orderLine.Id,
                Dish = DishMapper.MapModel(orderLine.Dish),
                Quantity = orderLine.Quantity,
                Price = orderLine.Price,
                DishState = orderLine.DishState
            };
        }

        public static MutateOrderDto MapCreateOrderDto(CreateOrderViewModel model)
        {
            MutateOrderDto dto = new MutateOrderDto()
            {
                Id = model.Id,
                TableId = model.TableId
            };
            foreach (var dish in model.Dishes)
            {
                OrderDishDto orderDishDto = MapOrderDishDto(dish);
                dto.Dishes.Add(orderDishDto);
            }
            return dto;
        }

        public static OrderDishDto MapOrderDishDto(OrderDishViewModel dish)
        {
            return new OrderDishDto()
            {
                Id = dish.Id,
                Quantity = dish.Quantity
            };
        }
    }
}
