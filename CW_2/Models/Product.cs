using System.ComponentModel.DataAnnotations;

namespace CW_2.Models
{
    /// <summary>
    /// Товар у каталозі.
    /// </summary>
    public class Product
    {
        /// <summary>Унікальний ідентифікатор товару. Призначається сервером при створенні.</summary>
        public int Id { get; set; }
        /// <summary>Назва товару, як вона показується покупцю.</summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>Категорія товару (наприклад, "Аксесуари" чи "Периферія").</summary>
        public string Category { get; set; } = string.Empty;
        /// <summary>Бренд товару. Необов'язкове поле — не в кожного товару є конкретний бренд.</summary>
        public string? Brand { get; set; }
        /// <summary>Ціна товару в гривнях.</summary>
        public decimal Price { get; set; }
        /// <summary>Чи є товар в наявності. За замовчуванням — так.</summary>
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
