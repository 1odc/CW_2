using CW_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ProductService _productService;

        public AdminController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            return Ok(new
            {
                TotalProducts = _productService.GetCount(),
                InStock = _productService.GetInStock().Count
            });
        }
        [HttpDelete("clear")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Clear()
        {
            _productService.Clear();
            return NoContent();
        }
        [HttpDelete("{id:int}")]    
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult DeleteById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.Delete(id);
            return NoContent();
        }
    }
}
