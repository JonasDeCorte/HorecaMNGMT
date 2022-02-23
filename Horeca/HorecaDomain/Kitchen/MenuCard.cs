namespace Domain.Kitchen
{
    public class MenuCard
    {
        public string Name { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Dish> Dishes { get; set; }

        public MenuCard()
        {

        }
    }
}
