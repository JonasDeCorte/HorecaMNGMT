namespace Horeca.Shared.Data.Entities
{
    public class MenuCard : BaseEntity
    {
        public string Name { get; set; }
        public List<Menu> Menus { get; set; } = new List<Menu>();
        public List<Dish> Dishes { get; set; } = new List<Dish>();
    }
}