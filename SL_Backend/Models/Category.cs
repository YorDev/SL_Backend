namespace SL_Backend.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }

}
