namespace SL_Backend.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductCategory
    {
        [Required]
        public long ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public long CategoryId { get; set; }

        public Category Category { get; set; }
    }

}
