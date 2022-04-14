namespace Horeca.Shared.Data.Entities
{
    public class BookingDetail : BaseEntity
    {
        private Schedule? _schedule;

        private Booking? _booking;

        public Booking Booking
        {
            set => _booking = value;
            get => _booking
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Booking));
        }

        public int BookingId { get; set; }

        public int ScheduleId { get; set; }

        public Schedule Schedule
        {
            set => _schedule = value;
            get => _schedule
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Schedule));
        }

        public int Pax { get; set; }
    }
}