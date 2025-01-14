﻿namespace SL_Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public long Id { get; set; }

        [Required]
        public long CustomerId { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
