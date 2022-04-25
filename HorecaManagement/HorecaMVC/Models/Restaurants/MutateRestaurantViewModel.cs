using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Restaurants
{
    public class MutateRestaurantViewModel : RestaurantViewModel
    {
        [Display(Name = "Owner Name")]
        [Required]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string OwnerName { get; set; }
    }
}
