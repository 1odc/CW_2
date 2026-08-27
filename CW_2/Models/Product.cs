using System.ComponentModel.DataAnnotations;

namespace CW_2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Brand { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; } = true;

    }
    public class CreateProductRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        [StringLength(50)]
        public string? Brand { get; set; }
        public bool InStock { get; set; } = true;
    }
}
