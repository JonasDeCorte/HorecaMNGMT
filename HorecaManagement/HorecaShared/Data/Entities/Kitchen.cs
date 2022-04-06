namespace Horeca.Shared.Data.Entities
{
    public class Kitchen : BaseEntity
    {
        public List<Order> Orders { get; set; } = new();
    }
}