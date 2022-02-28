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

        private readonly List<Ingredient> _ingredients = new();
        public IReadOnlyList<Ingredient> Ingredients => _ingredients.AsReadOnly();

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

        public void AddIngredient(Ingredient ingredient)
        {
            if (_ingredients.Contains(ingredient))
            {
                throw new ArgumentException($"{nameof(ingredient)} is already added to {Name}");
            }

            _ingredients.Add(ingredient);
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            if (!_ingredients.Contains(ingredient))

                throw new ArgumentException($"{nameof(ingredient)} is not in {Name}");

            _ingredients.Remove(ingredient);
        }
    }
}