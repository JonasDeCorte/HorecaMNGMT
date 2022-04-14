using Horeca.MVC.Models.Dishes;

namespace Horeca.MVC.Models.Orders
{
    public class CreateOrderViewModel
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public List<OrderDishViewModel> Dishes { get; set; } = new List<OrderDishViewModel>();
    }
}
