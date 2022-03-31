namespace Horeca.Shared.Data.Entities
{
    public class DishIngredient : BaseEntity
    {
        private Dish? _dish;
        private Ingredient? _ingredient;

        public int DishId { get; set; }

        public Dish Dish
        {
            set => _dish = value;
            get => _dish
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Dish));
        }

        public int IngredientId { get; set; }

        public Ingredient Ingredient
        {
            set => _ingredient = value;
            get => _ingredient
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Ingredient));
        }
    }
}