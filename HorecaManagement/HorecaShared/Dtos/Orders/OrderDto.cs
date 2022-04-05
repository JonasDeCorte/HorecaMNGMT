using Horeca.Shared.Dtos.Dishes;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.Shared.Dtos.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
    }

    public class MutateOrderDto : OrderDto
    {
        public List<OrderDishDto> Dishes { get; set; } = new();
    }

    public class GetOrderLinesByTableIdDto : OrderDto
    {
        public OrderState State { get; set; }

        public List<OrderLineDto> Lines { get; set; } = new();
    }

    public class OrderLineDto
    {
        public int Id { get; set; }
        public DishDto Dish { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public DishState DishState { get; set; }
    }
}