using ECart.Models;

namespace ECart.Dto
{
    public class CartItemDto
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
