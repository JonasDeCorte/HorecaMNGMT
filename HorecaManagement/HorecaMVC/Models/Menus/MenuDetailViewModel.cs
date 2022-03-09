using Horeca.Shared.Data.Entities;

namespace Horeca.MVC.Models.Menus
{
    public class MenuDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<Menu> Menus { get; set; } = new List<Menu>();
    }
}
