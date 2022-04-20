using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Dtos.Ingredients;
using Horeca.Shared.Dtos.Units;

namespace Horeca.MVC.Helpers.Mappers
{
    public static class IngredientMapper
    {
        public static IngredientViewModel MapModel(IngredientDto ingredientDto)
        {
            return new IngredientViewModel
            {
                IngredientId = ingredientDto.Id,
                Name = ingredientDto.Name,
                IngredientType = ingredientDto.IngredientType,
                BaseAmount = ingredientDto.BaseAmount,
                Unit = new UnitViewModel
                {
                    Id = ingredientDto.Unit.Id,
                    Name = ingredientDto.Unit.Name
                }
            };
        }

        public static CreateIngredientViewModel MapCreateIngredientModel(List<UnitDto> unitDtos)
        {
            CreateIngredientViewModel model = new CreateIngredientViewModel();
            foreach (var unitDto in unitDtos)
            {
                model.Units.Add(new UnitViewModel
                {
                    Id = unitDto.Id,
                    Name = unitDto.Name,
                });
            }
            return model;
        }

        public static MutateIngredientDto MapCreateIngredientDto(CreateIngredientViewModel ingredientModel)
        {
            return new MutateIngredientDto
            {
                Id = ingredientModel.IngredientId,
                Name = ingredientModel.Name,
                BaseAmount = ingredientModel.BaseAmount,
                IngredientType = ingredientModel.IngredientType,
                Unit = new UnitDto
                {
                    Id = ingredientModel.UnitId,
                    Name = ""
                },
            };
        }

        public static MutateIngredientDto MapUpdateIngredientDto(IngredientViewModel ingredientModel, IngredientDto ingredient)
        {
            return new MutateIngredientDto
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
        }
    }
}