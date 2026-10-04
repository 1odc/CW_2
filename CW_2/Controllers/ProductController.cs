using CW_2.Services;
using CW_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CW_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            if (min > max)
            {
                return BadRequest(new { error = $"min ({min}) не може бути більшим за max ({max})." });
            }
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
                Availability = request.Availability
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
        [HttpPatch("{id:int}/toggle-stock")]
        public ActionResult<Product> ToggleStock(int id)
        {
            var product = _productService.ToggleStock(id);
            return product == null ? NotFound() : Ok(product);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return _productService.Delete(id) ? NoContent() : NotFound();
        }

    }
}
