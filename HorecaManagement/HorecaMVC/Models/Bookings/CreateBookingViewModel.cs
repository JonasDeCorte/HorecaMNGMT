using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Bookings
{
    public class CreateBookingViewModel
    {
        public BookingInfoViewModel Booking { get; set; }

        public int ScheduleId { get; set; }

        [Display(Name = "Amount of people")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public int Pax { get; set; }
    }
}
