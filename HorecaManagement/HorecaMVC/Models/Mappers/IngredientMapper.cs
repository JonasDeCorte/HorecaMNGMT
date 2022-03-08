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
    }
}
