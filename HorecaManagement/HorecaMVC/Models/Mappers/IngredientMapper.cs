using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Dtos.Ingredients;
using Horeca.Shared.Dtos.Units;

namespace Horeca.MVC.Models.Mappers
{
    public static class IngredientMapper
    {
        public static IngredientViewModel MapModel(IngredientDto ingredient)
        {
            IngredientViewModel model = new IngredientViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                IngredientType = ingredient.IngredientType,
                BaseAmount = ingredient.BaseAmount,
                Unit = new UnitDto
                {
                    Id = ingredient.Unit.Id,
                    Name = ingredient.Unit.Name
                }
            };

            return model;
        }

        public static MutateIngredientDto MapCreateIngredient(IngredientViewModel ingredientModel)
        {
            MutateIngredientDto result = new MutateIngredientDto
            {
                Id = ingredientModel.Id,
                Name = ingredientModel.Name,
                BaseAmount = ingredientModel.BaseAmount,
                IngredientType = ingredientModel.IngredientType,
                Unit = new UnitDto
                {
                    Id = ingredientModel.Unit.Id,
                    Name = ingredientModel.Unit.Name,
                },
            };

            return result;
        }

        public static MutateIngredientDto MapUpdateIngredient(IngredientViewModel ingredientModel, IngredientDto ingredient)
        {
            MutateIngredientDto result = new MutateIngredientDto
            {
                Id = ingredient.Id,
                Name = ingredientModel.Name,
                IngredientType = ingredientModel.IngredientType,
                BaseAmount = ingredientModel.BaseAmount,
                Unit = new UnitDto
                {
                    Id = ingredient.Unit.Id,
                    Name = ingredientModel.Unit.Name,
                }
            };

            return result;
        }
    }
}
