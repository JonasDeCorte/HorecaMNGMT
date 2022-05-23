using Horeca.MVC.Models.Menus;
using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Models.MenuCards
{
    public class ExistingMenuCardMenusViewModel : MenuListViewModel
    {
      
        public int RestaurantId { get; set; }

        
        public int MenuCardId { get; set; }

        [Required(ErrorMessage = "MenuId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "MenuId Id can't be 0")]
        public int MenuId { get; set; }
    }
}