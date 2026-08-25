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

        public ActionResult<IReadOnlyCollection<Product>> GetAll()
        {
            return Ok(_productService.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id) {
            return _productService.GetById(id) == null ? NotFound() : Ok(_productService.GetById(id));
        }
        [HttpGet("count")]
        public ActionResult<int> GetCount()
        {
            return Ok(_productService.GetCount());
        }
    }
}
