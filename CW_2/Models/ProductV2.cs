namespace CW_2.Models
{
    /// <summary>
    /// Товар у форматі другої версії API (v2).
    /// </summary>
    /// <remarks>
    /// Модуль 3, тема "Версіонування": це той самий товар, що й <see cref="Product"/>,
    /// але з одним новим полем — <see cref="DiscountPercent"/>. У реальному проєкті
    /// саме так і виникають нові версії API: коли потрібно змінити форму відповіді
    /// так, щоб не зламати клієнтів, які вже працюють зі старою версією (v1).
    /// </remarks>
    public class ProductV2
    {
        /// <summary>Унікальний ідентифікатор товару.</summary>
        public int Id { get; set; }

        /// <summary>Назва товару.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Категорія товару.</summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>Ціна товару в гривнях (без урахування знижки).</summary>
        public decimal Price { get; set; }

        /// <summary>Чи є товар в наявності.</summary>
        public bool InStock { get; set; } = true;

        /// <summary>Бренд товару, якщо є.</summary>
        public string? Brand { get; set; }

        /// <summary>
        /// Поточний відсоток знижки на товар (0-100). З'явилось лише у v2 —
        /// у v1 такого поля немає взагалі.
        /// </summary>
        public int DiscountPercent { get; set; }
    }

}
