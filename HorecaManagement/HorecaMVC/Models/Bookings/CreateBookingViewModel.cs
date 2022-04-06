namespace Horeca.MVC.Models.Bookings
{
    public class CreateBookingViewModel
    {
        public BookingDetailViewModel Booking { get; set; }
        public int ScheduleId { get; set; }
        public int Pax { get; set; }
    }
}
