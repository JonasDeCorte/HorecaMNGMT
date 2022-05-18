using Horeca.MVC.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Restaurants
{
    public class MutateEmployeeViewModel
    {
        [Required(ErrorMessage = "RestaurantId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "RestaurantId Id can't be 0")]
        public int RestaurantId { get; set; }

        [Required(ErrorMessage = "EmployeeId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "EmployeeId Id can't be 0")]
        public string EmployeeId { get; set; }

        public List<UserViewModel> Employees { get; set; } = new List<UserViewModel>();
    }
}