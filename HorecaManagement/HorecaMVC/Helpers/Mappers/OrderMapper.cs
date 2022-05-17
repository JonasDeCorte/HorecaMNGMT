using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Orders;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Orders;
using Horeca.Shared.Dtos.Tables;

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

        public static CreateOrderViewModel MapCreateOrderModel(TableDto table, IEnumerable<DishDto> dishes, int varyingDishes)
        {
            CreateOrderViewModel model = new CreateOrderViewModel()
            {
                FloorplanId = table.FloorplanId,
                TableId = table.Id,
                Name = table.Name,
            };
            for (int i = 0; i < varyingDishes; i++)
            {
                model.Quantities.Add(1);
            }
            foreach (var dish in dishes)
            {
                OrderDishViewModel dishModel = DishMapper.MapOrderDishModel(dish);
                model.Dishes.Add(dishModel);
            }
            return model;
        }

        public static OrderLineViewModel MapOrderLineModel(OrderLineDto orderLine)
        {
            return new OrderLineViewModel()
            {
                Id = orderLine.Id,
                Dish = DishMapper.MapDishModel(orderLine.Dish),
                Quantity = orderLine.Quantity,
                Price = orderLine.Price,
                DishState = orderLine.DishState
            };
        }

        public static MutateOrderDto MapCreateOrderDto(CreateOrderViewModel model)
        {
            MutateOrderDto dto = new MutateOrderDto()
            {
                TableId = model.TableId
            };
            int i = 0;
            foreach (var dishId in model.DishId)
            {
                OrderDishDto orderDishDto = MapOrderDishDto(dishId, model.Quantities[i]);
                dto.Dishes.Add(orderDishDto);
                i++;
            }
            return dto;
        }

        public static OrderDishDto MapOrderDishDto(int dishId, int quantity)
        {
            return new OrderDishDto()
            {
                Id = dishId,
                Quantity = quantity,
            };
        }
    }
}
