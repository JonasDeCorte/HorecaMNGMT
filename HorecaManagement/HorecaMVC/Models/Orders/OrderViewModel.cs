using System.ComponentModel.DataAnnotations;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.MVC.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        [Required]
        public OrderState OrderState { get; set; }

        public List<OrderLineViewModel> Lines { get; set; } = new List<OrderLineViewModel>();
    }
}
