using Horeca.MVC.Models.Ingredients;
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
                Name = ingredientModel.UnitName
            };
        }

        internal static MutateUnitDto MapMutateUnitDto(UnitViewModel unit)
        {
            return new MutateUnitDto()
            {
                Id = unit.Id,
                Name = unit.Name
            };
        }
    }
}
