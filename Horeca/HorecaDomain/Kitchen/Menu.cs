using HorecaDomain.Common;

namespace Domain.Kitchen
{
    public class Menu : Entity
    {
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Menu()
        {
        }

        public Menu(string name)
        {
            Name = name;
        }
    }
}