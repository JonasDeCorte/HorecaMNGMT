using Horeca.MVC.Models.Accounts;
using Horeca.MVC.Models.MenuCards;
using Horeca.MVC.Models.Schedules;

namespace Horeca.MVC.Models.Restaurants
{
    public class RestaurantDetailViewModel : RestaurantViewModel
    {
        public List<RestaurantScheduleViewModel> RestaurantSchedules { get; set; } = new List<RestaurantScheduleViewModel>();
        public List<UserViewModel> Employees { get; set; } = new List<UserViewModel>();
        public List<MenuCardViewModel> MenuCards { get; set; } = new List<MenuCardViewModel>();
    }
}
