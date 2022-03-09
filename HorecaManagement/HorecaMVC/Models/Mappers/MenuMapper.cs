using Horeca.MVC.Models.Menus;
using Horeca.Shared.Data.Entities;

namespace HorecaMVC.Models.Mappers
{
    public static class MenuMapper
    {
        public static MenuViewModel MapModel(Menu menu)
        {
            MenuViewModel model = new MenuViewModel();

            model.Id = menu.Id;
            model.Name = menu.Name;
            model.Description = menu.Description;
            model.Category = menu.Category;

            return model;
        }
    }
}
