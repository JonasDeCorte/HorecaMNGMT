using Horeca.MVC.Models.Dishes;
using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Orders
{
    public class CreateOrderViewModel
    {
        public int FloorplanId { get; set; }

        public int TableId { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Name { get; set; }

        [Required, MinLength(1, ErrorMessage = "At least one item required")]
        public List<int> Quantities { get; set; } = new List<int>();

        [Required, MinLength(1, ErrorMessage = "At least one item required")]
        public List<int> DishId { get; set; } = new List<int>();

        public List<OrderDishViewModel> Dishes { get; set; } = new List<OrderDishViewModel>();
    }
}
