using Horeca.MVC.Models.Bookings;

namespace Horeca.MVC.Models.Schedules
{
    public class ScheduleDetailViewModel : ScheduleViewModel
    {
        public string RestaurantName { get; set; }

        public List<BookingViewModel> Bookings { get; set; } = new List<BookingViewModel>();
    }
}