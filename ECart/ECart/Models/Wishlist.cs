using System;
using System.Collections.Generic;

namespace ECart.Models
{
    public partial class Wishlist
    {
        public string WishlistId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
