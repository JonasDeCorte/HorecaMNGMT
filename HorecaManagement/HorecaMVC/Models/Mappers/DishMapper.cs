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
                Id = dish.Id,
                Name = dish.Name,
                Category = dish.Category,
                DishType = dish.DishType,
                Description = dish.Description
            };

            return model;
        }

        public static DishDetailViewModel MapDetailModel(Dish dish)
        {
            DishDetailViewModel model = new DishDetailViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Category = dish.Category,
                DishType = dish.DishType,
                Description = dish.Description
            };
            foreach (var ingredient in dish.Ingredients)
            {
                IngredientDto ingredientDto = IngredientMapper.MapIngredient(ingredient);
                IngredientViewModel ingredientModel = IngredientMapper.MapModel(ingredientDto);
                model.Ingredients.Add(ingredientModel);
            }

            return model;
        }

        public static Dish MapDishDetail(DishDto dishDto, DishIngredientsByIdDto ingredientList)
        {
            Dish dish = new Dish()
            {
                Id = dishDto.Id,
                Name = dishDto.Name,
                Category = dishDto.Category,
                DishType = dishDto.DishType,
                Description = dishDto.Description,
            };
            foreach(var ingredientDto in ingredientList.Ingredients)
            {
                Ingredient ingredient = new Ingredient
                {
                    Id = ingredientDto.Id,
                    Name = ingredientDto.Name,
                    IngredientType = ingredientDto.IngredientType,
                    BaseAmount = ingredientDto.BaseAmount,
                    Unit = new Unit
                    {
                        Name = ingredientDto.Unit.Name,
                        Id = ingredientDto.Unit.Id
                    }
                };
                dish.Ingredients.Add(ingredient);
            }
            return dish;
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

        public static MutateIngredientByDishDto MapCreateIngredient(int id, IngredientViewModel ingredient)
        {
            MutateIngredientByDishDto result = new MutateIngredientByDishDto
            {
                Id = id,
                Ingredient = new MutateIngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    BaseAmount = ingredient.BaseAmount,
                    IngredientType = ingredient.IngredientType,
                    Unit = ingredient.Unit
                }
            };

            return result;

        }

        public static MutateIngredientByDishDto MapUpdateIngredient(DishIngredientViewModel ingredient)
        {
            MutateIngredientByDishDto result = new MutateIngredientByDishDto
            {
                Id = ingredient.DishId,
                Ingredient = new MutateIngredientDto
                {
                    Id = ingredient.IngredientId,
                    Name = ingredient.Name,
                    BaseAmount = ingredient.BaseAmount,
                    IngredientType = ingredient.IngredientType,
                    Unit = ingredient.Unit
                }
            };

            return result;
        }

        public static DishIngredientViewModel MapMutateIngredientModel(int dishId, IngredientDto ingredient)
        {
            DishIngredientViewModel result = new DishIngredientViewModel
            {
                DishId = dishId,
                IngredientId = ingredient.Id,
                Name = ingredient.Name,
                IngredientType = ingredient.IngredientType,
                BaseAmount = ingredient.BaseAmount,
                Unit = new UnitDto
                {
                    Id = ingredient.Unit.Id,
                    Name = ingredient.Unit.Name
                }
            };

            return result;
        }
    }
}
