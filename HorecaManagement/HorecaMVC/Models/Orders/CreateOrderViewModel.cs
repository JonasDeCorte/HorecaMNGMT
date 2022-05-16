using Horeca.MVC.Models.Dishes;

namespace Horeca.MVC.Models.Orders
{
    public class CreateOrderViewModel
    {
        public int FloorplanId { get; set; }
        public int TableId { get; set; }
        public string Name { get; set; }

        public List<int> DishId { get; set; } = new List<int>();

        public List<OrderDishViewModel> Dishes { get; set; } = new List<OrderDishViewModel>();
    }
}
