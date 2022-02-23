using Domain.Kitchen;
using Domain.Restaurants;

namespace Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public Table Table { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();
        
        public Order()
        {

        }
    }
}
