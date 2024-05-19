namespace TechStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Technics Technics { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
    }
}
