namespace Horeca.Shared.Data.Entities
{
    public class Receipt
    {
        private readonly List<ReceiptLine> _lines = new();

        public IReadOnlyList<ReceiptLine> Lines => _lines.AsReadOnly();
        public decimal Total => _lines.Sum(x => x.Total);

        public ReceiptLine AddItem(Dish dish, int quantity)
        {
            var existingLine = _lines.SingleOrDefault(x => x.Dish.Equals(dish));
            if (existingLine is not null)
            {
                existingLine.IncreaseQuantity(quantity);
                return existingLine;
            }
            else
            {
                ReceiptLine line = new();
                line.Dish = dish;
                line.Quantity = quantity;
                _lines.Add(line);
                return line;
            }
        }

        public void RemoveLine(Dish dish)
        {
            ReceiptLine line = _lines.SingleOrDefault(l => l.Dish.Equals(dish));
            if (line != null)
            {
                _lines.Remove(line);
            }
        }

        public void Clear()
        {
            _lines.Clear();
        }
    }
}