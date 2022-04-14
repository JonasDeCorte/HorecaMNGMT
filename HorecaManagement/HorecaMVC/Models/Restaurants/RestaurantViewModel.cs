using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.Restaurants
{
    public class RestaurantViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
