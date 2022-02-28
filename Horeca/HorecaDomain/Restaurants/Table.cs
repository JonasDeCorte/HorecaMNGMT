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

        public List<Booking> Bookings { get; set; }

        public FloorPlan FloorPlan { get; set; }
        public int FloorPlanId { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        public Table()
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