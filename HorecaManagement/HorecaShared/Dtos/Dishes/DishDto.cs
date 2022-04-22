using Horeca.Shared.Dtos.Ingredients;

namespace Horeca.Shared.Dtos.Dishes
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DishType { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
    }

    public class MutateDishDto : DishDto
    {
        public int RestaurantId { get; set; }
    }

    public class OrderDishDto
    {
        public int Id { get; set; }

        public int Quantity { get; set; }
    }

    public class DeleteIngredientDishDto
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }

        public int RestaurantId { get; set; }
    }

    public class DishIngredientsByIdDto
    {
        public int Id { get; set; }

        public List<IngredientDto>? Ingredients { get; set; }
    }

    public class MutateIngredientByDishDto
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public MutateIngredientDto Ingredient { get; set; }
    }
}