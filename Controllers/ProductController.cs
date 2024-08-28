using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productervice;
        public ProductController(IProductService productService)
        {
            _productervice=productService;
        }

        [HttpGet("GetProductList")]
        public IActionResult GetProductList([FromQuery] string? userid,[FromQuery]Filter filter) 
        {
             // 之後可以驗證是否已登錄

            if(string.IsNullOrEmpty(filter.tag) && string.IsNullOrEmpty(filter.kind))
            {
                return Content($"no data\n{filter.kind}\n{filter.tag}");
            }


            var products = _productervice.GetProducts(userid, filter.kind, filter.tag);

            
            return Ok(products);
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
