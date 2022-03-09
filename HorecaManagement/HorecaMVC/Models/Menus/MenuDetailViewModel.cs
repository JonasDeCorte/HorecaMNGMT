using Horeca.MVC.Models.Dishes;

namespace Horeca.MVC.Models.Menus
{
    public class MenuDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<DishViewModel> Dishes { get; set; } = new List<DishViewModel>();
    }
}
