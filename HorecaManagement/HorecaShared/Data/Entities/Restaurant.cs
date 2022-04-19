namespace Horeca.Shared.Data.Entities
{
    public class Restaurant : BaseEntity
    {
        public string Name { get; set; }

        public List<RestaurantUser> Employees { get; set; } = new();

        public List<Order> Orders { get; set; } = new();
    }
}