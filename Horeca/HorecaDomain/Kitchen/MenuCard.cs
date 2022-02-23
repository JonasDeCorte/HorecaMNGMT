using Domain.Restaurants;
using HorecaDomain.Common;

namespace Domain.Kitchen
{
    public class MenuCard : Entity
    {
        public string Name { get; set; }
        public Restaurant Restaurant { get; set; }
        public List<Menu> Menus { get; set; } = new List<Menu>();
        public List<Dish> Dishes { get; set; } = new List<Dish>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private MenuCard()
        {
        }

        public MenuCard(string name)
        {
            Name = name;
        }
    }
}