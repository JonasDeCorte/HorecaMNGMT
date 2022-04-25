using Horeca.MVC.Models.Bookings;
using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Schedules
{
    public class ScheduleDetailViewModel : ScheduleViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string RestaurantName { get; set; }

        public List<BookingViewModel> Bookings { get; set; } = new List<BookingViewModel>();
    }
}