using EcommerceBackend.Controllers;

namespace EcommerceBackend.Models
{
 
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string? SelectColor { get; set; }

        public string? SelectSize { get; set; }
    }

}
