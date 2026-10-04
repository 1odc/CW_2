using CW_2.Services;
using CW_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CW_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Products")]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _productService;

        public ProductController(ProductService productService) {
            _productService = productService;
        }
        /// <summary>Повертає список товарів з опційним фільтром і сортуванням.</summary>
        /// <param name="category">Фільтр за категорією (точний збіг, чутливо до регістру).</param>
        /// <param name="minPrice">Мінімальна ціна товару.</param>
        /// <param name="sortBy">Сортування: "price" (за ціною) або "name" (за назвою).</param>
        /// <returns>Список товарів, що відповідають фільтрам.</returns>
        [HttpGet]
        public ActionResult<IReadOnlyCollection<Product>> GetAll(
            [FromQuery] string? category,
            [FromQuery] decimal? minPrice,
            [FromQuery] string? sortBy)
        {
            return Ok(_productService.GetAll(category, minPrice, sortBy));
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
        public ActionResult<List<Product>> GetInStock()
        {
            return Ok(_productService.GetInStock());
        }
        [HttpGet("brand/{brand}")]
        public ActionResult<List<Product>> GetBrand(string brand)
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
        [HttpPut("{id:int}")]
        public ActionResult UpdateProduct(int id, [FromBody] CreateProductRequest product)
        {
            bool success = _productService.Update(id, product);
            return success ? NoContent() : NotFound();
        }

    }
}
