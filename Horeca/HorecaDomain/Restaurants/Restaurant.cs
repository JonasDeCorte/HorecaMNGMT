using Domain.Kitchen;
using Domain.Users;

namespace Domain.Restaurants
{
    public class Restaurant
    {
        public string Name { get; set; }
        public List<MenuCard> MenuCard { get; set; } = new List<MenuCard>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<FloorPlan> FloorPlans { get; set; } = new List<FloorPlan>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
        
        public Restaurant(string name)
        {
            Name = name;
        }
    }
}
