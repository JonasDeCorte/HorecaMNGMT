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

        public static List<MenuCardViewModel> MapMenuCardModelList(List<MenuCardDto> menuCards)
        {
            List<MenuCardViewModel> result = new();
            foreach (var menuCard in menuCards)
            {
                result.Add(MapMenuCardModel(menuCard));
            }
            return result;
        }

        public static List<DishViewModel> MapRemainingDishesList(MenuCardsByIdDto menuListsDto, IEnumerable<DishDto> dishes)
        {
            List<DishViewModel> dishList = new();
            foreach (var dish in dishes)
            {
                if (!menuListsDto.Dishes.Any(item => item.Id == dish.Id))
                {
                    DishViewModel dishModel = DishMapper.MapDishModel(dish);
                    dishList.Add(dishModel);
                }
            }
            return dishList;
        }

        public static List<MenuViewModel> MapRemainingMenusList(MenuCardsByIdDto menuListsDto, IEnumerable<MenuDto> menus)
        {
            List<MenuViewModel> menuList = new();
            foreach (var menu in menus)
            {
                if (!menuListsDto.Dishes.Any(item => item.Id == menu.Id))
                {
                    MenuViewModel menuModel = MenuMapper.MapModel(menu);
                    menuList.Add(menuModel);
                }
            }
            return menuList;
        }

        public static MutateMenuCardDto MapMutateMenuCard(MenuCardViewModel menuCardModel, MenuCardDto menuCard)
        {
            return new MutateMenuCardDto
            {
                Id = menuCard.Id,
                Name = menuCardModel.Name
            };
        }

        public static MutateDishMenuCardDto MapMutateMenuCardDish(int id, int? restaurantId, DishViewModel dish)
        {
            return new MutateDishMenuCardDto
            {
                MenuCardId = id,
                RestaurantId = (int)restaurantId,
                Dish = new MutateDishDto
                {
                    Id = dish.DishId,
                    RestaurantId = (int)restaurantId,
                    Name = dish.Name,
                    DishType = dish.DishType,
                    Category = dish.Category,
                    Description = dish.Description,
                    Price = dish.Price,
                }
            };
        }

        public static MutateMenuMenuCardDto MapMutateMenuCardMenu(int id, int? restaurantId, MenuViewModel menu)
        {
            return new MutateMenuMenuCardDto
            {
                MenuCardId = id,
                RestaurantId = (int)restaurantId,
                Menu = new MutateMenuDto
                {
                    Id = menu.MenuId,
                    RestaurantId = (int)restaurantId,
                    Name = menu.Name,
                    Category = menu.Category,
                    Description = menu.Description,
                    Price = menu.Price,
                }
            };
        }
    }
}