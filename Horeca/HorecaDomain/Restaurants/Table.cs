using Domain.Orders;
using HorecaDomain.Common;
using HorecaDomain.Restaurants;

namespace Domain.Restaurants
{
    public class Table : Entity
    {
        public string TableNumber { get; set; }
        public int Seats { get; set; }
        public TableType TableType { get; set; }

        public Booking Booking { get; set; }
        public int BookingId { get; set; }
        public FloorPlan FloorPlan { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Table()
        {
        }

        public Table(string tableNumber, int seats, TableType tableType)
        {
            TableNumber = tableNumber;
            Seats = seats;
            TableType = tableType;
        }
    }
}