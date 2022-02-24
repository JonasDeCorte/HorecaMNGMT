using Ardalis.GuardClauses;
using Domain.Kitchen;
using Domain.Users;
using HorecaDomain.Common;

namespace Domain.Restaurants
{
    public class Restaurant : Entity
    {
        public string Name { get; set; }
        public List<MenuCard> MenuCard { get; set; } = new List<MenuCard>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<FloorPlan> FloorPlans { get; set; } = new List<FloorPlan>();
        public List<Employee> Employees { get; set; } = new List<Employee>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Restaurant()
        {
        }

        public Restaurant(string name)
        {
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        }
    }
}