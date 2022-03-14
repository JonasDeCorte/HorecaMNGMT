using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.MenuCards;
using Horeca.MVC.Models.Menus;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Models.Mappers
{
    public static class MenuCardMapper
    {
        public static MenuCardViewModel MapModel(MenuCard menuCard)
        {
            MenuCardViewModel model = new MenuCardViewModel();

            model.Id = menuCard.Id;
            model.Name = menuCard.Name;

            return model;
        }

        public static MenuCard MapMenuCard(MenuCardViewModel menuCardModel, MenuCard menuCard)
        {
            MenuCard result = menuCard;

            result.Name = menuCardModel.Name;

            return result;
        }
        public static MenuCardDetailViewModel MapDetailModel(MenuCard menuCard)
        {
            MenuCardDetailViewModel model = new MenuCardDetailViewModel();

            model.Id = menuCard.Id;
            model.Name = menuCard.Name;

            foreach (var dish in menuCard.Dishes)
            {
                DishViewModel dishModel = DishMapper.MapModel(dish);
                model.Dishes.Add(dishModel);
            }

            foreach (var menu in menuCard.Menus)
            {
                MenuViewModel menuModel = MenuMapper.MapModel(menu);
                model.Menus.Add(menuModel);
            }

            return model;
        }
        public static MutateDishMenuCardDto MapCreateDish(int id, DishViewModel dish)
        {
            MutateDishMenuCardDto result = new MutateDishMenuCardDto();
            result.MenuCardId = id;
            result.Dish = new MutateDishDto();
            result.Dish.Id = dish.Id;
            result.Dish.Name = dish.Name;
            result.Dish.DishType = dish.DishType;
            result.Dish.Category = dish.Category;
            result.Dish.Description = dish.Description;

            return result;
        }

        public static MutateMenuMenuCardDto MapCreateMenu(int id, MenuViewModel menu)
        {
            MutateMenuMenuCardDto result = new MutateMenuMenuCardDto();
            result.MenuCardId = id;
            result.Menu = new MutateMenuDto();
            result.Menu.Id = menu.Id;
            result.Menu.Name = menu.Name;
            result.Menu.Category = menu.Category;
            result.Menu.Description = menu.Description;

            return result;
        }
    }
}
