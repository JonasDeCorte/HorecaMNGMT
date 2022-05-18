using Horeca.MVC.Models.Menus;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.MenuCards
{
    public class ExistingMenuCardMenusViewModel : MenuListViewModel
    {
        [Required(ErrorMessage = "RestaurantId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "RestaurantId Id can't be 0")]
        public int RestaurantId { get; set; }

        [Required(ErrorMessage = "MenuCardId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MenuCardId Id can't be 0")]
        public int MenuCardId { get; set; }

        [Required(ErrorMessage = "MenuId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MenuId Id can't be 0")]
        public int MenuId { get; set; }
    }
}