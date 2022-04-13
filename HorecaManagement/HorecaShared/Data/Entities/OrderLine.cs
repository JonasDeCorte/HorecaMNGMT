using System.ComponentModel.DataAnnotations.Schema;
using static Horeca.Shared.Utils.Constants;

namespace Horeca.Shared.Data.Entities
{
    public class OrderLine : BaseEntity
    {
        public Dish Dish { get; set; }

        public int DishId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public DishState DishState { get; set; }
    }
}