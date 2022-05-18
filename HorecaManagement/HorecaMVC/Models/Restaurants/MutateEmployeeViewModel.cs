using Horeca.MVC.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Restaurants
{
    public class MutateEmployeeViewModel
    {
        public int RestaurantId { get; set; }

        [Required]
        [MinLength(10)]
        public string EmployeeId { get; set; }

        public List<UserViewModel> Employees { get; set; } = new List<UserViewModel>();
    }
}