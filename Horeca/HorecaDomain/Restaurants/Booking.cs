using Ardalis.GuardClauses;
using HorecaDomain.Common;

namespace Domain.Restaurants
{
    public class Booking : Entity
    {
        public string CustomerName { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int RequiredSeats { get; set; }

        public Restaurant Restaurant { get; set; }

        //public Table Table { get; set; }

        public int TableId { get; set; }

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Booking()
        {
        }

        public Booking(string customerName, DateTime arrivalTime, int requiredSeats, Restaurant restaurant, Table table)
        {
            CustomerName = Guard.Against.NullOrWhiteSpace(customerName, nameof(customerName));
            RequiredSeats = Guard.Against.NegativeOrZero(requiredSeats, nameof(requiredSeats));
            restaurant = Guard.Against.Null(restaurant, nameof(restaurant));
            //Table = Guard.Against.Null(table, nameof(table));
            ArrivalTime = arrivalTime;
        }
    }
}