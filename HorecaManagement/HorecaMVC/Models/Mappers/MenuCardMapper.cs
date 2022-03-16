﻿using Horeca.MVC.Models.Dishes;
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
        public static MenuCardViewModel MapModel(MenuCardDto menuCard)
        {
            MenuCardViewModel model = new MenuCardViewModel
            {
                Id = menuCard.Id,
                Name = menuCard.Name
            };

            return model;
        }

        public static MenuCardDetailViewModel MapDetailModel(MenuCard menuCard)
        {
            MenuCardDetailViewModel model = new MenuCardDetailViewModel
            {
                Id = menuCard.Id,
                Name = menuCard.Name
            };
            foreach (var dish in menuCard.Dishes)
            {
                DishDto dishDto = new DishDto
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Description = dish.Description,
                    Category = dish.Category,
                    DishType = dish.DishType
                };
                DishViewModel dishModel = DishMapper.MapModel(dishDto);
                model.Dishes.Add(dishModel);
            }
            foreach (var menu in menuCard.Menus)
            {
                MenuDto menuDto = new MenuDto
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    Category = menu.Category,
                    Description = menu.Description
                };
                MenuViewModel menuModel = MenuMapper.MapModel(menuDto);
                model.Menus.Add(menuModel);
            }

            return model;
        }
        public static MenuCardDishViewModel MapMutateMenuCardDishModel(int menuCardId, DishDto dish)
        {
            MenuCardDishViewModel result = new MenuCardDishViewModel
            {
                MenuCardId = menuCardId,
                DishId = dish.Id,
                Name = dish.Name,
                DishType = dish.DishType,
                Category = dish.Category,
                Description = dish.Description
            };
            return result;
        }

        public static MenuCardMenuViewModel MapMutateMenuCardMenuModel(int menuCardId, MenuDto menu)
        {
            MenuCardMenuViewModel result = new MenuCardMenuViewModel
            {
                MenuCardId = menuCardId,
                MenuId = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category
            };
            return result;
        }

        public static MenuCard MapMenuCardDetail(MenuCardDto menuCard, MenuCardsByIdDto menuCardLists)
        {
            MenuCard result = new MenuCard
            {
                Id = menuCard.Id,
                Name = menuCard.Name
            };
            foreach (var dishDto in menuCardLists.Dishes)
            {
                Dish dishResult = new Dish
                {
                    Id = dishDto.Id,
                    Name = dishDto.Name,
                    Description = dishDto.Description,
                    Category = dishDto.Category,
                    DishType = dishDto.DishType,
                };
                result.Dishes.Add(dishResult);
            }
            foreach (var menuDto in menuCardLists.Menus)
            {
                Menu menuResult = new Menu
                {
                    Id = menuDto.Id,
                    Name = menuDto.Name,
                    Description = menuDto.Description,
                    Category = menuDto.Category
                };
                result.Menus.Add(menuResult);
            }
            return result;
        }

        public static MutateMenuCardDto MapMutateMenuCard(MenuCardViewModel menuCardModel, MenuCardDto menuCard)
        {
            MutateMenuCardDto result = new MutateMenuCardDto
            {
                Id = menuCard.Id,
                Name = menuCardModel.Name
            };

            result.Name = menuCardModel.Name;

            return result;
        }

        public static MutateDishMenuCardDto MapCreateDish(int id, DishViewModel dish)
        {
            MutateDishMenuCardDto result = new MutateDishMenuCardDto
            {
                MenuCardId = id,
                Dish = new MutateDishDto
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    DishType = dish.DishType,
                    Category = dish.Category,
                    Description = dish.Description
                }
            };

            return result;
        }

        public static MutateMenuMenuCardDto MapCreateMenu(int id, MenuViewModel menu)
        {
            MutateMenuMenuCardDto result = new MutateMenuMenuCardDto
            {
                MenuCardId = id,
                Menu = new MutateMenuDto
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    Category = menu.Category,
                    Description = menu.Description
                }
            };

            return result;
        }

        public static MutateDishMenuCardDto MapUpdateDish(MenuCardDishViewModel model)
        {

            MutateDishMenuCardDto result = new MutateDishMenuCardDto
            {
                MenuCardId = model.MenuCardId,
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

        public static MutateMenuMenuCardDto MapUpdateMenu(MenuCardMenuViewModel model)
        {
            MutateMenuMenuCardDto result = new MutateMenuMenuCardDto
            {
                MenuCardId = model.MenuCardId,
                Menu = new MutateMenuDto
                {
                    Id = model.MenuId,
                    Name = model.Name,
                    Description = model.Description,
                    Category = model.Category
                }
            };
            return result;
        }
    }
}
