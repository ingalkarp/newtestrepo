using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECart.Models
{
    public partial class Product 
    {
        [Key]
        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Seller { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string CoverFileName { get; set; }
    }
}
