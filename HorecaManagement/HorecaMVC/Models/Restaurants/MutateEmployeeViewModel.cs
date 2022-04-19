using Horeca.MVC.Models.Accounts;

namespace Horeca.MVC.Models.Restaurants
{
    public class MutateEmployeeViewModel
    {
        public int RestaurantId { get; set; }

        public string EmployeeId { get; set; }

        public List<UserViewModel> Employees { get; set; } = new List<UserViewModel>();
    }
}
