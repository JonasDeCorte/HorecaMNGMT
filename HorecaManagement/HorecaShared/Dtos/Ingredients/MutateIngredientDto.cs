using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Dtos
{
    public class MutateIngredientDto
    {
        public string Name { get; set; }
        public string IngredientType { get; set; }

        public int BaseAmount { get; set; }

        public Unit Unit { get; set; }
    }
}