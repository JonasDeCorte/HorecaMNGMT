using Horeca.MVC.Models.Dishes;

namespace Horeca.MVC.Models.Menus
{
    public class ExistingMenuDishesViewModel : DishListViewModel
    {
        public int RestaurantId { get; set; }

        public int MenuId { get; set; }

        public int DishId { get; set; }
    }
}
