using Horeca.MVC.Models.Dishes;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.MenuCards
{
    public class ExistingMenuCardDishesViewModel : DishListViewModel
    {
        [Required(ErrorMessage = "RestaurantId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "RestaurantId Id can't be 0")]
        public int RestaurantId { get; set; }

        [Required(ErrorMessage = "MenuCardId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MenuCardId Id can't be 0")]
        public int MenuCardId { get; set; }

        [Required(ErrorMessage = "DishId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Dish Id can't be 0")]
        public int DishId { get; set; }
    }
}