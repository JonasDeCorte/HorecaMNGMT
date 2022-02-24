using Ardalis.GuardClauses;
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

        // moet een table booking obj bijhouden? idem met floorplan?
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
            TableNumber = Guard.Against.NullOrWhiteSpace(tableNumber, nameof(tableNumber));
            Seats = Guard.Against.NegativeOrZero(seats, nameof(seats));
            TableType = Guard.Against.Null(tableType, nameof(TableType));
        }
    }
}