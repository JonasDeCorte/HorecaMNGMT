namespace Horeca.Shared.Data.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new();

        public Restaurant? Restaurant { get; set; }

        public int? RestaurantId { get; set; }
    }
}