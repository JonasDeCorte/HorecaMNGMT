namespace Horeca.Shared.Data.Entities
{
    public class ReceiptLine
    {
        public Dish Dish { get; set; }

        public int Quantity { get; set; }
        public decimal Total => Dish.Price * Quantity;

        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}