namespace Horeca.Shared.Data.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public string IngredientType { get; set; }

        public int BaseAmount { get; set; }

        public Unit Unit { get; set; }
        public int UnitId { get; set; }
    }
}