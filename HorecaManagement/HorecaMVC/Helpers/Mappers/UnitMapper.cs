using Horeca.MVC.Models.Ingredients;
using Horeca.Shared.Dtos.Ingredients;
using Horeca.Shared.Dtos.Units;

namespace Horeca.MVC.Helpers.Mappers
{
    public class UnitMapper
    {
        public static UnitViewModel MapUnitModel(UnitDto? unitDto)
        {
            return new UnitViewModel
            {
                Id = unitDto.Id,
                Name = unitDto.Name
            };
        }

        public static UnitDto MapUnitDto(CreateIngredientViewModel ingredientModel)
        {
            return new UnitDto
            {
                Id = ingredientModel.UnitId,
                Name = ""
            };
        }

        internal static UnitDto MapUnitDto(IngredientViewModel ingredientModel, IngredientDto ingredient)
        {
            return new UnitDto
            {
                Id = ingredient.Unit.Id,
                Name = ingredientModel.Unit.Name,
            };
        }
    }
}
