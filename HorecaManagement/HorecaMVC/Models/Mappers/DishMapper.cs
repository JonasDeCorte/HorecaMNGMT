using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Ingredients;
using Horeca.Shared.Dtos.Units;

namespace Horeca.MVC.Models.Mappers
{
    public static class DishMapper
    {
        public static DishViewModel MapModel(DishDto dish)
        {
            DishViewModel model = new DishViewModel
            {
                DishId = dish.Id,
                Name = dish.Name,
                Category = dish.Category,
                DishType = dish.DishType,
                Description = dish.Description
            };

            return model;
        }

        public static DishIngredientViewModel MapDishIngredientModel(MutateIngredientByDishDto dishIngredientDto)
        {
            DishIngredientViewModel model = new DishIngredientViewModel
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
            return model;
        }

        public static DishDetailViewModel MapDetailModel(Dish dish)
        {
            DishDetailViewModel model = new DishDetailViewModel
            {
                DishId = dish.Id,
                Name = dish.Name,
                Category = dish.Category,
                DishType = dish.DishType,
                Description = dish.Description
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
            DishIngredientViewModel result = new DishIngredientViewModel
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

            return result;
        }

        public static Dish MapDish(DishDto dishDto)
        {
            Dish dish = new Dish
            {
                Id = dishDto.Id,
                Name = dishDto.Name,
                Description = dishDto.Description,
                Category = dishDto.Category,
                DishType = dishDto.DishType,
            };
            return dish;
        }

        public static DishIngredient MapDishIngredient(IngredientDto ingredientDto, DishDto dishDto)
        {
            DishIngredient dishIngredient = new DishIngredient
            {
                DishId = dishDto.Id,
                Dish = new Dish
                {
                    Name = dishDto.Name,
                    Description = dishDto.Description,
                    Category = dishDto.Category,
                    DishType = dishDto.DishType
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
            return dishIngredient;
        }

        public static Dish MapDishDetail(DishDto dishDto, DishIngredientsByIdDto ingredientList)
        {
            Dish dish = MapDish(dishDto);
            foreach(var ingredientDto in ingredientList.Ingredients)
            {
                DishIngredient ingredient = MapDishIngredient(ingredientDto, dishDto);
                dish.DishIngredients.Add(ingredient);
            }
            return dish;
        }

        public static List<IngredientViewModel> MapRemainingIngredientsList(DishIngredientsByIdDto dishIngredientDto, IEnumerable<IngredientDto> ingredients)
        {
            List<IngredientViewModel> ingredientList = new List<IngredientViewModel>();
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
            DishDto dishDto = new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Category = dish.Category,
                DishType = dish.DishType
            };
            return dishDto;
        }

        public static MutateDishDto MapMutateDish(DishViewModel dishModel, DishDto dish)
        {
            MutateDishDto result = new MutateDishDto
            {
                Id = dish.Id,
                Name = dishModel.Name,
                DishType = dishModel.DishType,
                Description = dishModel.Description,
                Category = dishModel.Category,
            };

            return result;
        }

        public static MutateIngredientByDishDto MapDishIngredientDto(DishIngredient dishIngredient)
        {
            MutateIngredientByDishDto mutateIngredientDto = new MutateIngredientByDishDto
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

            return mutateIngredientDto;
        }

        public static MutateIngredientByDishDto MapMutateDishIngredientDto(int id, IngredientViewModel ingredient)
        {
            MutateIngredientByDishDto result = new MutateIngredientByDishDto
            {
                Id = id,
                Ingredient = new MutateIngredientDto
                {
                    Id = ingredient.IngredientId,
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
            return result;
        }
    }
}
