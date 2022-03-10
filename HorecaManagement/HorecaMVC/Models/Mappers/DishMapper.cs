using Horeca.MVC.Models.Dishes;
using Horeca.MVC.Models.Ingredients;
using Horeca.MVC.Models.Mappers;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos;
using Horeca.Shared.Dtos.Dishes;

namespace Horeca.MVC.Models.Mappers
{
    public static class DishMapper
    {
        public static DishViewModel MapModel(Dish dish)
        {
            DishViewModel model = new DishViewModel();

            model.Id = dish.Id;
            model.Name = dish.Name;
            model.Category = dish.Category;
            model.DishType = dish.DishType;
            model.Description = dish.Description;

            return model;
        }
        public static DishDetailViewModel MapDetailModel(Dish dish)
        {
            DishDetailViewModel model = new DishDetailViewModel();

            model.Id = dish.Id;
            model.Name = dish.Name;
            model.Category = dish.Category;
            model.DishType = dish.DishType;
            model.Description = dish.Description;

            foreach (var ingredient in dish.Ingredients)
            {
                IngredientViewModel ingredientModel = IngredientMapper.MapModel(ingredient);
                model.Ingredients.Add(ingredientModel);
            }

            return model;
        }

        public static MutateIngredientByDishDto MapCreateIngredient(int id, IngredientViewModel ingredient)
        {
            MutateIngredientByDishDto result = new MutateIngredientByDishDto();
            result.Id = id;
            result.Ingredient = new MutateIngredientDto();
            result.Ingredient.Id = ingredient.Id;
            result.Ingredient.Name = ingredient.Name;
            result.Ingredient.BaseAmount = ingredient.BaseAmount;
            result.Ingredient.IngredientType = ingredient.IngredientType;
            result.Ingredient.Unit = ingredient.Unit;

            return result;
        }

        public static Dish MapDish(DishViewModel dishModel, Dish dish)
        {
            Dish result = dish;

            result.Name = dishModel.Name;
            result.Category = dishModel.Category;
            result.DishType = dishModel.DishType;
            result.Description = dishModel.Description;

            return result;
        }
    }
}
