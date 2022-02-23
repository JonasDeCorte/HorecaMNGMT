using Domain.Restaurants;

namespace Domain.Kitchen
{
    public class MenuCard
    {
        public string Name { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<Menu> Menus { get; set; } = new List<Menu>();
        public List<Dish> Dishes { get; set; } = new List<Dish>();

        public MenuCard(string name)
        {
            Name = name;
        }
    }
}
