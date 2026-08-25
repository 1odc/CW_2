using CW_2.Models;

namespace CW_2.Services
{
    public class ProductService
    {
        private readonly List<Product> _productsList = new()
        {
            new Product { Id = 1, Name = "Миша ", Category = "Аксесуари", Price = 699m, Brand = "Logitech" },
            new Product { Id = 2, Name = "Клавіатура", Category = "Аксесуари", Price = 799m, Brand = "Logitech" },
            new Product { Id = 3, Name = "Монітор 27\"", Category = "Периферія", Price = 6999m, Brand = "Razor", InStock = false }
        };
        public IReadOnlyCollection<Product> GetAll()
        {
            return _productsList;
        }
        public int GetCount()
        {
            return _productsList.Count;
        }
        public Product? GetById(int id)
        {
            return _productsList.FirstOrDefault(p => p.Id == id);
        }
        public List<Product>? GetByCategory(string category)
        {
            List<Product> result = _productsList
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return result;

        }
        public Product? GetLowestPrice()
        {
            return _productsList.OrderBy(p => p.Price).FirstOrDefault();
        }
        public Product? GetHighestPrice()
        {
            return _productsList.OrderByDescending(p => p.Price).FirstOrDefault();
        }
        public List<Product>? GetInStock()
        {
            return _productsList.Where(p => p.InStock).ToList();
        }
        public List<Product>? GetBrand(string brand)
        {
            List<Product> result = _productsList
                .Where(p => p.Brand != null && p.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return result;
        }
        public List<Product>? GetMoreExpensive(decimal minPrice)
        {
            return _productsList.Where(p => p.Price > minPrice).ToList();
        }
    }

}
