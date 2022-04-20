namespace Horeca.MVC.Models.MenuCards
{
    public class MutateRestaurantMenuCardViewModel
    {
        public int RestaurantId { get; set; }

        public int MenuCardId { get; set; }

        public List<MenuCardViewModel> MenuCards { get; set; } = new List<MenuCardViewModel>();
    }
}
