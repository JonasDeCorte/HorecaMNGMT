namespace Horeca.MVC.Models.Dishes
{
    public class ExistingDishesViewModel : DishListViewModel
    {
        public int RestaurantId { get; set; }

        public int MenuId { get; set; }

        public int DishId { get; set; }
    }
}
