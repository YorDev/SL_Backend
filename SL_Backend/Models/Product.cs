namespace SL_Backend.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }

}
