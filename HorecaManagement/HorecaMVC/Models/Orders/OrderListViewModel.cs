namespace Horeca.MVC.Models.Orders
{
    public class OrderListViewModel
    {
        public int RestaurantId { get; set; }

        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}
