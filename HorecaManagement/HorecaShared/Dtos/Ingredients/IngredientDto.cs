using Horeca.Shared.Dtos.Units;

namespace Horeca.Shared.Dtos.Ingredients
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IngredientType { get; set; }
        public UnitDto? Unit { get; set; }

        public int BaseAmount { get; set; }
    }

    public class MutateIngredientDto : IngredientDto
    {
        public int RestaurantId { get; set; }
    }
}