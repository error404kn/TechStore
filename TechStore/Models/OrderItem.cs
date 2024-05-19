namespace TechStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }
        public int TechnicsId { get; set; }
        public Order Order { get; set; }
        public Technics Technics { get; set; }
    }
}
