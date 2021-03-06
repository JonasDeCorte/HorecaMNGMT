using Horeca.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Restaurants
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [StringLength(50, ErrorMessage = ErrorConstants.StringLength50)]
        public string Name { get; set; }
    }
}