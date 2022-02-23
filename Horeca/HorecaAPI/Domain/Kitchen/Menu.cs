namespace Domain.Kitchen
{
    public class Menu
    {
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();

        public Menu(string name)
        {
            Name = name;
        }
    }
}
