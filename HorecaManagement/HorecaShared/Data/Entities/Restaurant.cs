namespace Horeca.Shared.Data.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        public List<RestaurantUser> Employees { get; set; } = new();

        public List<MenuCard> MenuCards { get; set; } = new();

        public Kitchen? Kitchen { get; set; }
        public int? KitchenId { get; set; }
    }
}