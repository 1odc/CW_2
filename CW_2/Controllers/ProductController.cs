using CW_2.Services;
using CW_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CW_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService) {
            _productService = productService;
        }

        [HttpGet]

        public ActionResult<IReadOnlyCollection<Product>> GetAll(
            [FromQuery] string? category,
            [FromQuery] decimal? minPrice,
            [FromQuery] string? sortBy)
        {
            IEnumerable<Product> result = _productService.GetAll();

            if (!string.IsNullOrEmpty(category))
            {
                result = result.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                result = result.Where(p => p.Price >= minPrice.Value);
            }

            result = sortBy?.ToLower() switch
            {
                "price" => result.OrderBy(p => p.Price),
                "name" => result.OrderBy(p => p.Name),
                _ => result
            };

            return Ok(result.ToList());
        }
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id) {
            return _productService.GetById(id) == null ? NotFound() : Ok(_productService.GetById(id));
        }
        [HttpGet("count")]
        public ActionResult<int> GetCount()
        {
            return Ok(_productService.GetCount());
        }
        [HttpGet("in-stock")]
        public ActionResult<int> GetInStock()
        {
            return Ok(_productService.GetInStock());
        }
        [HttpGet("brand/{brand}")]
        public ActionResult<int> GetBrand(string brand)
        {
            return Ok(_productService.GetBrand(brand));
        }
        [HttpGet("price-range/{min:decimal}/{max:decimal}")]
        public ActionResult<List<Product>> GetByPriceRange(decimal min, decimal max)
        {
            return Ok(_productService.GetByPriceRange(min, max));
        }
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                Brand = request.Brand,
                InStock = request.InStock
            };
            _productService.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] CreateProductRequest product)
        {
            bool success = _productService.Update(id, product);
            return success ? NoContent() : NotFound();
        }

    }
}
