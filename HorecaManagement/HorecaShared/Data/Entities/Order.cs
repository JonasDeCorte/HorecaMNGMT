namespace Horeca.Shared.Data.Entities
{
    public class Order : BaseEntity
    {
        public List<OrderLine> OrderLines { get; set; } = new();
        public Table Table { get; set; }
        public int TableId { get; set; }
    }
}