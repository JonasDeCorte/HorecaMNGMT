using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Bookings
{
    public class BookingDetailViewModel : BookingViewModel
    {
        [Display(Name = "Phone number")]
        [Required]
        public string PhoneNo { get; set; }
        [Display(Name = "Contact name")]
        [Required]
        public string FullName { get; set; }
    }
}
