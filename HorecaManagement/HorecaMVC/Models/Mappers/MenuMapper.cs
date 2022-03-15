using Horeca.MVC.Models.Dishes;
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

            return model;
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

        public static Menu MapMenuDetail(MenuDto menu, MenuDishesByIdDto dishList)
        {
            Menu result = new Menu
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                Category = menu.Category,
            };

            foreach (var dishDto in dishList.Dishes)
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
            return result;
        }

        public static MutateDishMenuDto MapCreateDish(int id, DishViewModel dish)
        {
            MutateDishMenuDto result = new MutateDishMenuDto
            {
                Id = id,
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
    }
}
