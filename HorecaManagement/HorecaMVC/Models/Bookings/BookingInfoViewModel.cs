using Horeca.MVC.Helpers.Attributes;
using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Bookings
{
    public class BookingInfoViewModel
    {
        public string UserID { get; set; }

        [Display(Name = "Contact Name")]
        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string FullName { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression(@"\+[0-9]+", ErrorMessage = ErrorConstants.Invalid)]
        public string PhoneNo { get; set; }

        [Display(Name = "Booking Date")]
        [Required]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Check-in time")]
        [Required]
        [DateSmallerThan("CheckOut", ErrorMessage = ErrorConstants.CheckInSmaller)]
        public DateTime? CheckIn { get; set; }

        [Display(Name = "Check-out time")]
        [Required]
        public DateTime? CheckOut { get; set; }
    }
}
