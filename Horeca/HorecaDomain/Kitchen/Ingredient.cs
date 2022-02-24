using HorecaDomain.Common;
using Ardalis.GuardClauses;

namespace Domain.Kitchen
{
    public class Ingredient : Entity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public IngredientType IngredientType { get; set; }

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
    }
}