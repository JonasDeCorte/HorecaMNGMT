namespace Horeca.MVC.Models.Bookings
{
    public class CreateBookingViewModel
    {
        public BookingInfoViewModel Booking { get; set; }
        public int ScheduleId { get; set; }
        public int Pax { get; set; }
    }
}
