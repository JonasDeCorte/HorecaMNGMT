using Horeca.MVC.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Bookings
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string UserID { get; set; }

        public string BookingNo { get; set; }
        [Display(Name = "Booking date")]
        [Required]
        public DateTime BookingDate { get; set; }
        [Display(Name = "Check-in time")]
        [Required]
        [DateSmallerThan("CheckOut", ErrorMessage = "Check-in time must be earlier than Check-out time.")]
        public DateTime? CheckIn { get; set; }
        [Display(Name = "Check-out time")]
        [Required]
        public DateTime? CheckOut { get; set; }
        [Display(Name = "Status")]
        [Required]
        public string BookingStatus { get; set; }
    }
}
