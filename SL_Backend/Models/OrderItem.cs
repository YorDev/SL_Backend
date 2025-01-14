namespace SL_Backend.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderItem
    {
        public long Id { get; set; }

        [Required]
        public long OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public long ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }

}
