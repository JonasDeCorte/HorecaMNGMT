using Horeca.MVC.Helpers.Attributes;
using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Bookings
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        public string BookingNo { get; set; }

        public string UserID { get; set; }

        public int ScheduleId { get; set; }

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

        [Display(Name = "Status")]
        [Required]
        public string BookingStatus { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"\+[0-9]+", ErrorMessage = ErrorConstants.Invalid)]
        public string PhoneNo { get; set; }

        [Display(Name = "Contact name")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Amount of people")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.AboveZero)]
        public int Pax { get; set; }
    }
}
