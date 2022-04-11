namespace Horeca.Shared.Data.Entities
{
    public class Menu : BaseEntity
    {
        public string Description { get; set; }
        public string Category { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();
    }
}