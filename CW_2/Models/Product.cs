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
}
