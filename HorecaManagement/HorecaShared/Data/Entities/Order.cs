using static Horeca.Shared.Utils.Constants;

namespace Horeca.Shared.Data.Entities
{
    public class Order : BaseEntity
    {
        public int TableId { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new();

        public OrderState OrderState { get; set; } = OrderState.Begin;
    }
}