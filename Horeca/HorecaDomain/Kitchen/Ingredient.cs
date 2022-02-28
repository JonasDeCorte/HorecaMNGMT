using HorecaDomain.Common;
using Ardalis.GuardClauses;

namespace Domain.Kitchen
{
    public class Ingredient : Entity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        //public string Unit { get; set; }
        public IngredientType IngredientType { get; set; }

        private readonly List<Dish> _dishes = new();
        public IReadOnlyList<Dish> Dishes => _dishes;

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Ingredient()
        {
        }

        public Ingredient(string name, int amount, IngredientType ingredientType)
        {
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Amount = Guard.Against.NegativeOrZero(amount, nameof(amount));
            IngredientType = Guard.Against.Null(ingredientType, nameof(ingredientType));
        }

        public void AddDish(Dish dish)
        {
            if (_dishes.Contains(dish))
            {
                throw new ArgumentException($"{nameof(dish)} is already added to {Name}");
            }

            _dishes.Add(dish);
        }

        public void RemoveDish(Dish dish)
        {
            if (!_dishes.Contains(dish))

                throw new ArgumentException($"{nameof(dish)} is not in {Name}");

            _dishes.Remove(dish);
        }
    }
}