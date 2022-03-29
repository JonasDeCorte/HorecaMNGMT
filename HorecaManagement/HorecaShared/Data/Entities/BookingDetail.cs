namespace Horeca.Shared.Data.Entities
{
    public class BookingDetail : BaseEntity
    {
        private RestaurantSchedule? _restaurantSchedule;

        private Booking? _booking;

        public Booking Booking
        {
            set => _booking = value;
            get => _booking
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Booking));
        }

        public int BookingId { get; set; }

        public int RestaurantScheduleId { get; set; }

        public RestaurantSchedule RestaurantSchedule
        {
            set => _restaurantSchedule = value;
            get => _restaurantSchedule
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(RestaurantSchedule));
        }

        public int Pax { get; set; }
    }
}