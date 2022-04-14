using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Bookings
{
    public class CreateBookingViewModel
    {
        public BookingInfoViewModel Booking { get; set; }
        public int ScheduleId { get; set; }
        [Display(Name = "Amount of people")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be higher than 0.")]
        public int Pax { get; set; }
    }
}
