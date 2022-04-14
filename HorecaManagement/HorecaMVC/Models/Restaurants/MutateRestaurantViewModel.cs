using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Restaurants
{
    public class MutateRestaurantViewModel : RestaurantViewModel
    {
        [Display(Name = "Owner Name")]
        [Required]
        public string OwnerName { get; set; }
    }
}
