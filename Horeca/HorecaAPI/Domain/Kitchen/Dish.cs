namespace Domain.Kitchen
{
    public class Dish
    {
        public string Name { get; set; }
        public DishType DishType { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public Dish(string name, DishType dishType)
        {
            Name = name;
            DishType = dishType;
        }
    }
}
