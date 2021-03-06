namespace Horeca.Shared.Data.Entities
{
    public class Menu : BaseEntity
    {
        public string Description { get; set; }
        public string Category { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public List<MenuDish> MenuDishes { get; set; } = new();

        public Restaurant? Restaurant { get; set; }
        public int? RestaurantId { get; set; }
    }
}