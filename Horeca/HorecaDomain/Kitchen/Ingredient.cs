using HorecaDomain.Common;

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

        public Ingredient(string name, IngredientType ingredientType)
        {
            Name = name;
            IngredientType = ingredientType;
        }
    }
}