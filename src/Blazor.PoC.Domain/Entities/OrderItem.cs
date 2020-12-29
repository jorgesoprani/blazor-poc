namespace Blazor.PoC.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
