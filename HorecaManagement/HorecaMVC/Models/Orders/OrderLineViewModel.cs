using Horeca.MVC.Models.Dishes;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.MVC.Models.Orders
{
    public class OrderLineViewModel
    {
        public int Id { get; set; }
        public DishViewModel Dish { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public DishState DishState { get; set; }
    }
}
