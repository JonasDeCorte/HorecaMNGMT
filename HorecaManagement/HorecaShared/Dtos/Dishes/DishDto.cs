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
    }

    public class MutateDishDto : DishDto
    {
    }

    public class DeleteIngredientDishDto
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }
    }

    public class DishDtoDetailDto : DishDto
    {
        public List<Ingredient>? Ingredients { get; set; }
    }

    public class DishIngredientsByIdDto
    {
        public int Id { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
    }

    public class MutateIngredientByDishDto
    {
        public int Id { get; set; }
        public MutateIngredientDto Ingredient { get; set; }
    }
}