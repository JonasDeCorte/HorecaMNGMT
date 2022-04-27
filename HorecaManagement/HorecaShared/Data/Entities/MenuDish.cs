namespace Horeca.Shared.Data.Entities
{
    public class MenuDish : BaseEntity
    {
        private Dish? _dish;
        private Menu? _menu;

        public int DishId { get; set; }

        public Dish Dish
        {
            set => _dish = value;
            get => _dish
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Dish));
        }

        public int menuId { get; set; }

        public Menu Menu
        {
            set => _menu = value;
            get => _menu
                   ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Menu));
        }
    }
}