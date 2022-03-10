using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Menus;

namespace Horeca.MVC.Models.MenuCards
{
    public class MenuCardDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DishViewModel> Dishes { get; set; } = new List<DishViewModel>();
        public List<MenuViewModel> Menus { get; set; } = new List<MenuViewModel>();
    }
}
