using Horeca.MVC.Helpers.Attributes;
using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Bookings
{
    public class CreateBookingViewModel
    {
        public string UserID { get; set; }

        [Display(Name = "Booking date")]
        [Required]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Check-in time")]
        [Required]
        [DateSmallerThan("CheckOut", ErrorMessage = ErrorConstants.CheckInSmaller)]
        public DateTime? CheckIn { get; set; }

        [Display(Name = "Check-out time")]
        [Required]
        public DateTime? CheckOut { get; set; }

        [Display(Name = "Contact name")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"\+[0-9]+", ErrorMessage = ErrorConstants.Invalid)]
        public string PhoneNo { get; set; }

        public int ScheduleId { get; set; }

        public int ScheduleCapacity { get; set; }

        [Display(Name = "Amount of people")]
        [Required]
        [Capacity("ScheduleCapacity", ErrorMessage = ErrorConstants.AmountOfPerson)]
        public int Pax { get; set; }
    }
}