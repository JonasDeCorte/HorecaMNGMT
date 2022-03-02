using Horeca.Shared.Data.Entities;

namespace Horeca.Shared.Dtos.Dishes
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DishType { get; set; }
        public string Category { get; set; }

        public string Description { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}