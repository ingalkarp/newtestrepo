using System;
using System.Collections.Generic;

namespace ECart.Models
{
    public partial class CartItems
    {
        public int CartItemId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
