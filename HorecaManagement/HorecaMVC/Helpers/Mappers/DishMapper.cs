using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Ingredients;
using Horeca.Shared.Dtos.Units;

namespace Horeca.MVC.Helpers.Mappers
{
    public static class DishMapper
    {
        public static DishViewModel MapModel(DishDto dish)
        {
            DishViewModel model = new()
            {
                DishId = dish.Id,
                Name = dish.Name,
                Category = dish.Category,
                DishType = dish.DishType,
                Description = dish.Description,
                Price = dish.Price,
            };

            return model;
        }

        public static DishListViewModel MapDishListModel(IEnumerable<DishDto> dishes)
        {
            DishListViewModel model = new();
            foreach (var item in dishes)
            {
                DishViewModel dishModel = MapModel(item);
                model.Dishes.Add(dishModel);
            }
            return model;
        }

        public static DishIngredientViewModel MapDishIngredientModel(MutateIngredientByDishDto dishIngredientDto)
        {
            return new DishIngredientViewModel
            {
                DishId = dishIngredientDto.Id,
                IngredientId = dishIngredientDto.Ingredient.Id,
                Name = dishIngredientDto.Ingredient.Name,
                IngredientType = dishIngredientDto.Ingredient.IngredientType,
                BaseAmount = dishIngredientDto.Ingredient.BaseAmount,
                Unit = new UnitViewModel
                {
                    Id = dishIngredientDto.Ingredient.Unit.Id,
                    Name = dishIngredientDto.Ingredient.Unit.Name
                }
            };
        }

        public static DishDetailViewModel MapDetailModel(Dish dish)
        {
            DishDetailViewModel model = new()
            {
                DishId = dish.Id,
                Name = dish.Name,
                Category = dish.Category,
                DishType = dish.DishType,
                Description = dish.Description,
                Price = dish.Price
            };
            foreach (var ingredient in dish.DishIngredients)
            {
                MutateIngredientByDishDto mutateIngredientDto = MapDishIngredientDto(ingredient);
                DishIngredientViewModel ingredientModel = MapDishIngredientModel(mutateIngredientDto);
                model.Ingredients.Add(ingredientModel);
            }

            return model;
        }

        public static DishIngredientViewModel MapUpdateIngredientModel(int dishId, IngredientDto ingredient)
        {
            return new DishIngredientViewModel
            {
                DishId = dishId,
                IngredientId = ingredient.Id,
                Name = ingredient.Name,
                IngredientType = ingredient.IngredientType,
                BaseAmount = ingredient.BaseAmount,
                Unit = new UnitViewModel
                {
                    Id = ingredient.Unit.Id,
                    Name = ingredient.Unit.Name
                }
            };
        }

        public static List<DishViewModel> MapDishModelList(List<Dish> dishes)
        {
            List<DishViewModel> list = new();
            foreach (var dish in dishes)
            {
                DishDto dishDto = MapDishDto(dish);
                DishViewModel dishModel = MapModel(dishDto);
                list.Add(dishModel);
            }
            return list;
        }

        public static Dish MapDish(DishDto dishDto)
        {
            return new Dish
            {
                Id = dishDto.Id,
                Name = dishDto.Name,
                Description = dishDto.Description,
                Category = dishDto.Category,
                DishType = dishDto.DishType,
                Price = dishDto.Price,
            };
        }

        public static DishIngredient MapDishIngredient(IngredientDto ingredientDto, DishDto dishDto)
        {
            return new DishIngredient
            {
                DishId = dishDto.Id,
                Dish = new Dish
                {
                    Name = dishDto.Name,
                    Description = dishDto.Description,
                    Category = dishDto.Category,
                    DishType = dishDto.DishType,
                    Price = dishDto.Price
                },
                IngredientId = ingredientDto.Id,
                Ingredient = new Ingredient
                {
                    Name = ingredientDto.Name,
                    IngredientType = ingredientDto.IngredientType,
                    BaseAmount = ingredientDto.BaseAmount,
                    Unit = new Unit
                    {
                        Id = ingredientDto.Unit.Id,
                        Name = ingredientDto.Unit.Name
                    }
                }
            };
        }

        public static Dish MapDishDetail(DishDto dishDto, DishIngredientsByIdDto ingredientList)
        {
            Dish dish = MapDish(dishDto);
            foreach (var ingredientDto in ingredientList.Ingredients)
            {
                DishIngredient ingredient = MapDishIngredient(ingredientDto, dishDto);
                dish.DishIngredients.Add(ingredient);
            }
            return dish;
        }

        public static List<IngredientViewModel> MapRemainingIngredientsList(DishIngredientsByIdDto dishIngredientDto, IEnumerable<IngredientDto> ingredients)
        {
            List<IngredientViewModel> ingredientList = new();
            foreach (var ingredient in ingredients)
            {
                IngredientViewModel ingredientModel = IngredientMapper.MapModel(ingredient);
                if (!dishIngredientDto.Ingredients.Any(item => item.Id == ingredient.Id))
                {
                    ingredientList.Add(ingredientModel);
                }
            }
            return ingredientList;
        }

        public static DishDto MapDishDto(Dish dish)
        {
            return new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Category = dish.Category,
                DishType = dish.DishType,
                Price = dish.Price,
            };
        }

        public static MutateDishDto MapMutateDish(DishViewModel dishModel, DishDto dish)
        {
            return new MutateDishDto
            {
                Id = dish.Id,
                Name = dishModel.Name,
                DishType = dishModel.DishType,
                Description = dishModel.Description,
                Category = dishModel.Category,
                Price = dishModel.Price,
            };
        }

        public static MutateIngredientByDishDto MapDishIngredientDto(DishIngredient dishIngredient)
        {
            return new MutateIngredientByDishDto
            {
                Id = dishIngredient.DishId,
                Ingredient = new MutateIngredientDto
                {
                    Id = dishIngredient.IngredientId,
                    Name = dishIngredient.Ingredient.Name,
                    IngredientType = dishIngredient.Ingredient.IngredientType,
                    BaseAmount = dishIngredient.Ingredient.BaseAmount,
                    Unit = new UnitDto
                    {
                        Id = dishIngredient.Ingredient.Unit.Id,
                        Name = dishIngredient.Ingredient.Unit.Name
                    }
                }
            };
        }

        public static MutateIngredientByDishDto MapMutateDishIngredientDto(int id, int? restaurantId, IngredientViewModel ingredient)
        {
            return new MutateIngredientByDishDto
            {
                Id = id,
                RestaurantId = (int)restaurantId,
                Ingredient = new MutateIngredientDto
                {
                    Id = ingredient.IngredientId,
                    RestaurantId = (int)restaurantId,
                    Name = ingredient.Name,
                    BaseAmount = ingredient.BaseAmount,
                    IngredientType = ingredient.IngredientType,
                    Unit = new UnitDto
                    {
                        Id = ingredient.Unit.Id,
                        Name = ingredient.Unit.Name
                    }
                }
            };
        }

        public static MutateIngredientByDishDto MapCreateDishIngredientDto(int id, int? restaurantId, CreateIngredientViewModel ingredient)
        {
            return new MutateIngredientByDishDto
            {
                Id = id,
                RestaurantId = (int)restaurantId,
                Ingredient = new MutateIngredientDto
                {
                    Id = ingredient.IngredientId,
                    RestaurantId = (int)restaurantId,
                    Name = ingredient.Name,
                    IngredientType = ingredient.IngredientType,
                    BaseAmount = ingredient.BaseAmount,
                    Unit = UnitMapper.MapUnitDto(ingredient)
                }
            };
        }
    }
}