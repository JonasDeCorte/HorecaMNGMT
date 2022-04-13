using Horeca.MVC.Models.Accounts;
using Horeca.MVC.Models.MenuCards;
using Horeca.MVC.Models.Schedules;

namespace Horeca.MVC.Models.Restaurants
{
    public class RestaurantDetailViewModel : RestaurantViewModel
    {
        public RestaurantScheduleListViewModel RestaurantScheduleListViewModel { get; set; } = new();
        public List<UserViewModel> Employees { get; set; } = new();
        public List<MenuCardViewModel> MenuCards { get; set; } = new();
    }
}