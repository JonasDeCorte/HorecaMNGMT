namespace Domain.Restaurants
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int RequiredSeats { get; set; }

        public Restaurant Restaurant { get; set; }
        public Table Table { get; set; }

        public Booking(string customerName, DateTime arrivalTime, int requiredSeats)
        {
            CustomerName = customerName;
            ArrivalTime = arrivalTime;
            RequiredSeats = requiredSeats;
        }
    }
}
