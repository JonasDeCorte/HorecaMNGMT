using HorecaDomain.Common;

namespace Domain.Restaurants
{
    public class Booking : Entity
    {
        public string CustomerName { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int RequiredSeats { get; set; }

        public Restaurant Restaurant { get; set; }
        public Table Table { get; set; }
        public int TableId { get; set; }

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Booking()
        {
        }

        public Booking(string customerName, DateTime arrivalTime, int requiredSeats)
        {
            CustomerName = customerName;
            ArrivalTime = arrivalTime;
            RequiredSeats = requiredSeats;
        }
    }
}