using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: BaseController
    {
        private readonly IProductService _productervice;
        private readonly IRedisService _redisService;

        // 是否已驗證
        //private bool IsAuthenticated => HttpContext.Items.ContainsKey("IsAuthenticated") && Convert.ToBoolean(HttpContext.Items["IsAuthenticated"]);


        public ProductController(IProductService productService,IRedisService redisService)
        {
            _productervice=productService;
            _redisService=redisService;
        }

        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList([FromQuery]Filter filter) 
        {

            string? userid=null;

            // 之後可以驗證是否已登錄
            if (SessionId != null)
            {
                userid = await _redisService.GetUserInfoAsync(SessionId);
            }
            
         

            if(string.IsNullOrEmpty(filter.tag) && string.IsNullOrEmpty(filter.kind))
            {            
                return Ok(Fail("請求類型不得為空"));
            }


            var products = _productervice.GetProducts(userid, filter.kind, filter.tag);

            var resp =Success(products);

            
            return Ok(resp);
        }



        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] string productId)
        {
            // 之後可以驗證是否已登錄
            string? userid = null;

            // 之後可以驗證是否已登錄
            if (SessionId != null)
            {
                userid = await _redisService.GetUserInfoAsync(SessionId);
            }

            // 可以驗證product 不存在的反回 code msg data
            var product = _productervice.GetProductById(userid, productId);

            var resp = Success(product);
            return Ok(resp);
        }


        [HttpPost("AddNewProduct")]
        public IActionResult AddNewProduct([FromBody] Product product)
        {
            return Content("ok");
        }

        [HttpPost("ModifyProduct")]
        public IActionResult ModifyProduct([FromBody] Product product)
        {
            return Content("ok");
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct([FromBody] Product product)
        {
            return Content("ok");
        }

        [HttpGet("GetRecommendationProduct")]
        public async Task<IActionResult> GetRecommendationProduct([FromQuery] string productId)
        {
            // 之後可以驗證是否已登錄
            string? userid = null;

            // 之後可以驗證是否已登錄
            if (SessionId != null)
            {
                userid = await _redisService.GetUserInfoAsync(SessionId);
            }

            var products = _productervice.GetRecommendationProduct(userid, productId);
            var resp = Success(products);
            return Ok(resp);
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
