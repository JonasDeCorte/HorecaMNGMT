using HorecaDomain.Common;

namespace Domain.Kitchen
{
    public class Dish : Entity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DishType DishType { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        /// <summary>
        /// Entity Framework Constructor
        /// </summary>
        private Dish()
        {
        }

        public Dish(string name, DishType dishType)
        {
            Name = name;
            DishType = dishType;
        }
    }
}