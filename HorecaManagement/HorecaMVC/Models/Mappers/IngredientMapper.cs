using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Data.Entities;

namespace Horeca.MVC.Models.Mappers
{
    public static class IngredientMapper
    {
        public static IngredientViewModel MapModel(Ingredient ingredient)
        {
            IngredientViewModel model = new IngredientViewModel();

            model.Id = ingredient.Id;
            model.Name = ingredient.Name;
            model.IngredientType = ingredient.IngredientType;
            model.BaseAmount = ingredient.BaseAmount;
            model.Unit = ingredient.Unit;

            return model;
        }

        public static Ingredient MapCreateIngredient(IngredientViewModel ingredientModel)
        {
            Ingredient result = new Ingredient();

            result.Name = ingredientModel.Name;
            result.BaseAmount = ingredientModel.BaseAmount;
            result.IngredientType = ingredientModel.IngredientType;
            result.Unit = ingredientModel.Unit;

            return result;
        }

        public static Ingredient MapIngredient(IngredientViewModel ingredientModel, Ingredient ingredient)
        {
            Ingredient result = ingredient;

            result.Name = ingredientModel.Name;
            result.IngredientType = ingredientModel.IngredientType;
            result.BaseAmount = ingredientModel.BaseAmount;
            result.Unit.Name = ingredientModel.Unit.Name;
            result.Unit.IsEnabled = true;

            return result;
        }
    }
}
