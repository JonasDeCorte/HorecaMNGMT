namespace Horeca.Shared.Data.Entities
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string DishType { get; set; }

        public decimal Price { get; set; }
        public List<DishIngredient> DishIngredients { get; set; } = new();

        public Restaurant? Restaurant { get; set; }

        public int? RestaurantId { get; set; }
    }
}