using Horeca.MVC.Models.Dishes;

namespace Horeca.MVC.Models.MenuCards
{
    public class ExistingMenuCardDishesViewModel : DishListViewModel
    {
        public int RestaurantId { get; set; }

        public int MenuCardId { get; set; }

        public int DishId { get; set; }
    }
}
