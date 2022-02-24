using HorecaDomain.Common;
using Ardalis.GuardClauses;

namespace Domain.Kitchen
{
    public class Dish : Entity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DishType DishType { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Dish()
        {
        }

        public Dish(string name, string category, string description, DishType dishType)
        {
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Category = Guard.Against.NullOrWhiteSpace(category, nameof(category));
            Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
            DishType = Guard.Against.Null(dishType, nameof(DishType));
        }
    }
}