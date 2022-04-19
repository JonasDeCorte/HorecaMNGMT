namespace Horeca.Shared.Data.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public string IngredientType { get; set; }

        public int BaseAmount { get; set; }

        public Unit Unit { get; set; }
        public int UnitId { get; set; }

        public Restaurant? Restaurant { get; set; }

        public int? RestaurantId { get; set; }
        public List<DishIngredient> Dishes { get; set; } = new();
    }
}