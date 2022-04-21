using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.MenuCards;
using Horeca.MVC.Models.Menus;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.MenuCards;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Helpers.Mappers
{
    public static class MenuCardMapper
    {
        public static MenuCardViewModel MapMenuCardModel(MenuCardDto menuCard)
        {
            return new MenuCardViewModel
            {
                Id = menuCard.Id,
                Name = menuCard.Name
            };
        }

        public static MenuCardDetailViewModel MapMenuCardDetailModel(MenuCard menuCard)
        {
            MenuCardDetailViewModel model = new()
            {
                Id = menuCard.Id,
                Name = menuCard.Name
            };
            model.Dishes = DishMapper.MapDishModelList(menuCard.Dishes);
            model.Menus = MenuMapper.MapMenuModelList(menuCard.Menus);

            return model;
        }

        public static MenuCardDishViewModel MapMutateMenuCardDishModel(int menuCardId, DishDto dish)
        {
            return new MenuCardDishViewModel
            {
                MenuCardId = menuCardId,
                DishId = dish.Id,
                Name = dish.Name,
                DishType = dish.DishType,
                Category = dish.Category,
                Description = dish.Description,
                Price = dish.Price,
            };
        }

        public static MenuCardMenuViewModel MapMutateMenuCardMenuModel(int menuCardId, MenuDto menu)
        {
            return new MenuCardMenuViewModel
            {
                MenuCardId = menuCardId,
                MenuId = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category,
                Price = menu.Price,
            };
        }

        public static MenuCard MapMenuCardDetail(MenuCardDto menuCard, MenuCardsByIdDto menuCardLists)
        {
            MenuCard result = new()
            {
                Id = menuCard.Id,
                Name = menuCard.Name
            };
            foreach (var dishDto in menuCardLists.Dishes)
            {
                Dish dishResult = DishMapper.MapDish(dishDto);
                result.Dishes.Add(dishResult);
            }
            foreach (var menuDto in menuCardLists.Menus)
            {
                Menu menuResult = MenuMapper.MapMenu(menuDto);
                result.Menus.Add(menuResult);
            }
            return result;
        }

        internal static List<MenuCardViewModel> MapMenuCardModelList(List<MenuCardDto> menuCards)
        {
            List<MenuCardViewModel> result = new();
            foreach (var menuCard in menuCards)
            {
                result.Add(MapMenuCardModel(menuCard));
            }
            return result;
        }

        public static MutateMenuCardDto MapMutateMenuCard(MenuCardViewModel menuCardModel, MenuCardDto menuCard)
        {
            return new MutateMenuCardDto
            {
                Id = menuCard.Id,
                Name = menuCardModel.Name
            };
        }

        public static MutateDishMenuCardDto MapMutateMenuCardDish(int id, DishViewModel dish)
        {
            return new MutateDishMenuCardDto
            {
                MenuCardId = id,
                Dish = new MutateDishDto
                {
                    Id = dish.DishId,
                    Name = dish.Name,
                    DishType = dish.DishType,
                    Category = dish.Category,
                    Description = dish.Description,
                    Price = dish.Price,
                }
            };
        }

        public static MutateMenuMenuCardDto MapMutateMenuCardMenu(int id, MenuViewModel menu)
        {
            return new MutateMenuMenuCardDto
            {
                MenuCardId = id,
                Menu = new MutateMenuDto
                {
                    Id = menu.MenuId,
                    Name = menu.Name,
                    Category = menu.Category,
                    Description = menu.Description,
                    Price = menu.Price,
                }
            };
        }
    }
}