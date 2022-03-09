using Horeca.MVC.Models.Dishes;
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

        public static MenuDetailViewModel MapDetailModel(Menu menu)
        {
            MenuDetailViewModel model = new MenuDetailViewModel();

            model.Id = menu.Id;
            model.Name = menu.Name;
            model.Description = menu.Description;
            model.Category = menu.Category;

            foreach (var dish in menu.Dishes)
            {
                DishViewModel dishModel = DishMapper.MapModel(dish);
                model.Dishes.Add(dishModel);
            }

            return model;
        }
    }
}
