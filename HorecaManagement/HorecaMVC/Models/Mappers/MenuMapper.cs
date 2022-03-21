﻿using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Menus;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;

namespace Horeca.MVC.Models.Mappers
{
    public static class MenuMapper
    {
        public static MenuViewModel MapModel(MenuDto menu)
        {
            MenuViewModel model = new MenuViewModel
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category
            };

            return model;
        }

        public static MenuDetailViewModel MapDetailModel(Menu menu)
        {
            MenuDetailViewModel model = new MenuDetailViewModel
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category,
            };
            foreach (var dish in menu.Dishes)
            {
                DishDto dishDto = DishMapper.MapDishDto(dish);
                DishViewModel dishModel = DishMapper.MapModel(dishDto);
                model.Dishes.Add(dishModel);
            }

            return model;
        }

        public static MenuDishViewModel MapMutateDishModel(int menuId, DishDto dish)
        {
            MenuDishViewModel result = new MenuDishViewModel
            {
                MenuId = menuId,
                DishId = dish.Id,
                Name = dish.Name,
                DishType = dish.DishType,
                Category = dish.Category,
                Description = dish.Description
            };
            return result;
        }

        public static Menu MapMenu(MenuDto menuDto)
        {
            Menu result = new Menu
            {
                Id = menuDto.Id,
                Name = menuDto.Name,
                Description = menuDto.Description,
                Category = menuDto.Category,
            };
            return result;
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
            MenuDto menuDto = new MenuDto
            {
                Id = menu.Id,
                Name = menu.Name,
                Category = menu.Category,
                Description = menu.Description
            };
            return menuDto;
        }

        public static MutateMenuDto MapMutateMenu(MenuViewModel menuModel, MenuDto menu)
        {
            MutateMenuDto result = new MutateMenuDto
            {
                Id = menu.Id,
                Name = menuModel.Name,
                Description = menuModel.Description,
                Category = menuModel.Category,
            };

            return result;
        }

        public static MutateDishMenuDto MapCreateDish(int id, DishViewModel model)
        {
            MutateDishMenuDto result = new MutateDishMenuDto
            {
                Id = id,
                Dish = new MutateDishDto
                {
                    Id = model.Id,
                    Name = model.Name,
                    DishType = model.DishType,
                    Category = model.Category,
                    Description = model.Description
                }
            };

            return result;
        }

        public static MutateDishMenuDto MapUpdateDish(MenuDishViewModel model)
        {
            MutateDishMenuDto result = new MutateDishMenuDto
            {
                Id = model.MenuId,
                Dish = new MutateDishDto
                {
                    Id = model.DishId,
                    Name = model.Name,
                    DishType = model.DishType,
                    Category = model.Category,
                    Description = model.Description
                }
            };

            return result;
        }
    }
}
