namespace Domain.Kitchen
{
    public class Dish
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DishType DishType { get; set; }
        
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Menu> Menus { get; set; } = new List<Menu>();
        public List<MenuCard> MenuCards { get; set; } = new List<MenuCard>();
        
        public Dish(string name, DishType dishType)
        {
            Name = name;
            DishType = dishType;
        }
    }
}
