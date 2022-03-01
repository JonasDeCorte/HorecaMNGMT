using Horeca.Shared.Dtos.Units;

namespace Horeca.Shared.Dtos
{
    public class UpdateIngredientDto
    {
        public string Name { get; set; }
        public string IngredientType { get; set; }

        public int BaseAmount { get; set; }

        public UpdateUnitDto Unit { get; set; }
    }
}