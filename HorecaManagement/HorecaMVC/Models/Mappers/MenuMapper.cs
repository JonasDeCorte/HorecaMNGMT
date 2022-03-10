using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Menus;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Models.Mappers
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

        public static Menu MapMenu(MenuViewModel menuModel, Menu menu)
        {
            Menu result = menu;

            result.Name = menuModel.Name;
            result.Description = menuModel.Description;
            result.Category = menuModel.Category;

            return result;
        }
        public static MutateDishMenuDto MapCreateDish(int id, DishViewModel dish)
        {
            MutateDishMenuDto result = new MutateDishMenuDto();
            result.Id = id;
            result.Dish = new MutateDishDto();
            result.Dish.Id = dish.Id;
            result.Dish.Name = dish.Name;
            result.Dish.DishType = dish.DishType;
            result.Dish.Category = dish.Category;
            result.Dish.Description = dish.Description;

            return result;
        }
    }
}
