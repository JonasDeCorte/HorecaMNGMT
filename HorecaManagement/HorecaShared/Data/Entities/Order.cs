namespace Horeca.Shared.Data.Entities
{
    public class Order : BaseEntity
    {
        public int TableId { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new();
    }
}