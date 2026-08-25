namespace CW_2.Services
{
    public class CategoryStatsService
    {
        private readonly ProductService _productService;
        public CategoryStatsService(ProductService productService)
        {
            _productService = productService;
        }
        public Dictionary<string, int> GetCountByCategory()
        {
            return _productService.GetAll()
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
