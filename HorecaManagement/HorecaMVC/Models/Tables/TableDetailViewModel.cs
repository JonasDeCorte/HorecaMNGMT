using Horeca.MVC.Models.Orders;

namespace Horeca.MVC.Models.Tables
{
    public class TableDetailViewModel : TableViewModel
    {
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public int ExistingDishes { get; set; }
    }
}
