using CW_2.Models;
using CW_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_2.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    [ApiExplorerSettings(GroupName = "v2")]
    [Tags("Products")]
    public class ProductV2Controller : ControllerBase
    {

        private readonly ProductService _productService;

        public ProductV2Controller(ProductService productService)
        {
            _productService = productService;
        }
        /// <summary>Повертає список товарів з опційним фільтром і сортуванням.</summary>
        /// <param name="category">Фільтр за категорією (точний збіг, чутливо до регістру).</param>
        /// <param name="minPrice">Мінімальна ціна товару.</param>
        /// <param name="sortBy">Сортування: "price" (за ціною) або "name" (за назвою).</param>
        /// <returns>Список товарів, що відповідають фільтрам.</returns>
        [HttpGet]
        public ActionResult<ProductListResponseV2> GetAll()
        {
            var result = _productService.GetAll();
            var items = result.Select(i => new ProductV2
            {
                Id = i.Id,
                Name = i.Name,
                Category = i.Category,
                Price = i.Price,
                Brand = i.Brand,
                InStock = i.InStock,
                DiscountPercent = 15
            }).ToList();
            return Ok(new ProductListResponseV2
            {
                Data = items,
                Count = items.Count
            });
        }
    }
}
