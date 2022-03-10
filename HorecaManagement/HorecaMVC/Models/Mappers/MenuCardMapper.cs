using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.MenuCards;
using Horeca.MVC.Models.Menus;
using Horeca.Shared.Data.Entities;

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
    }
}
