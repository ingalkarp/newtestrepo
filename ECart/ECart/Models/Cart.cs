﻿using System;
using System.Collections.Generic;

namespace ECart.Models
{
    public partial class Cart
    {
        public string CartId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
