namespace Domain.Kitchen
{
    public class Ingredient
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public IngredientType IngredientType { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();

        public Ingredient(string name, IngredientType ingredientType)
        {
            Name = name;
            IngredientType = ingredientType;
        }
    }
}
