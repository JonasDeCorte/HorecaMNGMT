using Horeca.MVC.Models.Menus;

namespace Horeca.MVC.Models.MenuCards
{
    public class ExistingMenuCardMenusViewModel : MenuListViewModel
    {
        public int RestaurantId { get; set; }

        public int MenuCardId { get; set; }

        public int MenuId { get; set; }
    }
}
