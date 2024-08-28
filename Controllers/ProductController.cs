using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        [HttpGet("GetProductList")]
        public IActionResult GetProductList([FromQuery]Filter filter) 
        {

            return Content($"{filter.kind}\n{filter.tag}");
        }

        [HttpPost("AddNewProduct")]
        public IActionResult AddNewProduct([FromBody] Product product)
        {
            return Content("ok");
        }

        [HttpDelete("ModifyProduct")]
        public IActionResult ModifyProduct([FromBody] Product product)
        {
            return Content("ok");
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct([FromBody] Product product)
        {
            return Content("ok");
        }

        [HttpDelete("GetRecommendationProduct")]
        public IActionResult GetRecommendationProduct([FromBody] Product product)
        {
            return Content("ok");
        }

    }

    public class Filter
    {
        // 衣服總類
        public string? kind { get; set; }

        // 特定的衣服類型
        public string? tag { get; set; }
    }

    public class Product
    {
        
    }
}
