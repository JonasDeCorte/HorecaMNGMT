using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Menus;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Helpers.Mappers
{
    public static class MenuMapper
    {
        public static MenuViewModel MapModel(MenuDto menu)
        {
            return new MenuViewModel
            {
                MenuId = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category,
                Price = menu.Price,
            };
        }

        public static MenuDetailViewModel MapDetailModel(Menu menu)
        {
            MenuDetailViewModel model = new()
            {
                MenuId = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category,
                Price = menu.Price,
            };
            model.Dishes = DishMapper.MapDishModelList(menu.Dishes);
            return model;
        }

        public static MenuDishViewModel MapMutateDishModel(int menuId, DishDto dish)
        {
            return new MenuDishViewModel
            {
                MenuId = menuId,
                DishId = dish.Id,
                Name = dish.Name,
                DishType = dish.DishType,
                Category = dish.Category,
                Description = dish.Description,
                Price = dish.Price,
            };
        }

        public static List<MenuViewModel> MapMenuModelList(List<Menu> menus)
        {
            List<MenuViewModel> result = new();
            foreach (var menu in menus)
            {
                MenuDto menuDto = MapMenuDto(menu);
                MenuViewModel menuModel = MapModel(menuDto);
                result.Add(menuModel);
            }
            return result;
        }

        public static Menu MapMenu(MenuDto menuDto)
        {
            return new Menu
            {
                Id = menuDto.Id,
                Name = menuDto.Name,
                Description = menuDto.Description,
                Category = menuDto.Category,
                Price = menuDto.Price,
            };
        }

        public static Menu MapMenuDetail(MenuDto menuDto, MenuDishesByIdDto dishList)
        {
            Menu result = MapMenu(menuDto);
            foreach (var dishDto in dishList.Dishes)
            {
                Dish dishResult = DishMapper.MapDish(dishDto);
                result.Dishes.Add(dishResult);
            }
            return result;
        }

        public static MenuDto MapMenuDto(Menu menu)
        {
            return new MenuDto
            {
                Id = menu.Id,
                Name = menu.Name,
                Category = menu.Category,
                Description = menu.Description,
                Price = menu.Price,
            };
        }

        public static MutateMenuDto MapMutateMenu(MenuViewModel menuModel, int? restaurantId)
        {
            return new MutateMenuDto
            {
                RestaurantId = (int)restaurantId,
                Id = menuModel.MenuId,
                Name = menuModel.Name,
                Description = menuModel.Description,
                Category = menuModel.Category,
                Price = menuModel.Price,
            };
        }

        public static MutateDishMenuDto MapMutateMenuDish(MenuDishViewModel model, int? restaurantId)
        {
            return new MutateDishMenuDto
            {
                Id = model.MenuId,
                RestaurantId = (int)restaurantId,
                Dish = new MutateDishDto
                {
                    Id = model.DishId,
                    RestaurantId = (int)restaurantId,
                    Name = model.Name,
                    DishType = model.DishType,
                    Category = model.Category,
                    Description = model.Description,
                    Price = model.Price,
                }
            };
        }
    }
}