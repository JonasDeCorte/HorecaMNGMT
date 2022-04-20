using Horeca.MVC.Models.Dishes;
using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.MVC.Models.Orders
{
    public class OrderLineViewModel
    {
        public int Id { get; set; }

        public DishViewModel Dish { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public decimal Price { get; set; }

        [Required]
        public DishState DishState { get; set; }
    }
}
