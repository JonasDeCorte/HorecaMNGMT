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
                Unit = UnitMapper.MapUnitModel(ingredientDto.Unit)
            };
        }

        public static CreateIngredientViewModel MapEditIngredientModel(IngredientDto ingredientDto, List<UnitDto> unitDtos)
        {
            CreateIngredientViewModel model = new CreateIngredientViewModel()
            {
                IngredientId = ingredientDto.Id,
                Name = ingredientDto.Name,
                IngredientType = ingredientDto.IngredientType,
                BaseAmount = ingredientDto.BaseAmount,
                UnitId = ingredientDto.Unit.Id
            };
            foreach (var unitDto in unitDtos)
            {
                model.Units.Add(UnitMapper.MapUnitModel(unitDto));
            }
            return model;
        }

        public static CreateIngredientViewModel MapCreateIngredientModel(List<UnitDto> unitDtos)
        {
            CreateIngredientViewModel model = new CreateIngredientViewModel();
            foreach (var unitDto in unitDtos)
            {
                model.Units.Add(UnitMapper.MapUnitModel(unitDto));
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
                Unit = UnitMapper.MapUnitDto(ingredientModel)
            };
        }

        public static MutateIngredientDto MapUpdateIngredientDto(CreateIngredientViewModel ingredientModel, IngredientDto ingredient)
        {
            return new MutateIngredientDto
            {
                Id = ingredient.Id,
                Name = ingredientModel.Name,
                IngredientType = ingredientModel.IngredientType,
                BaseAmount = ingredientModel.BaseAmount,
                Unit = UnitMapper.MapUnitDto(ingredientModel)
            };
        }
    }
}