using Horeca.MVC.Models.Dishes;

namespace Horeca.MVC.Models.Menus
{
    public class MenuDetailViewModel : MenuViewModel
    {
        public List<DishViewModel> Dishes { get; set; } = new List<DishViewModel>();
    }
}
