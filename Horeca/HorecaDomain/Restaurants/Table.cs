using Domain.Orders;
using HorecaDomain.Restaurants;

namespace Domain.Restaurants
{
    public class Table
    {
        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public TableType TableType { get; set; }

        public Booking Booking { get; set; }
        public FloorPlan FloorPlan { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        
        public Table(int tableNumber, int seats, TableType tableType)
        {
            TableNumber = tableNumber;
            Seats = seats;
            TableType = tableType;
        }

    }
}
