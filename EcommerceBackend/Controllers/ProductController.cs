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


            if (string.IsNullOrEmpty(filter.tag) && string.IsNullOrEmpty(filter.kind))
            {            
                return Fail("請求類型不得為空");
            }

            string? userid = UserInfo!=null? UserInfo.UserId:null;


            var result = _productervice.GetProducts(userid, filter.kind, filter.tag);

            if (!result.IsSuccess)
            {
                return Fail();
            }


            return Success(result.Data);


        }



        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] int productId)
        {

            string? userid = UserInfo != null ? UserInfo.UserId : null;
            // 可以驗證product 不存在的反回 code msg data
            var result = _productervice.GetProductById(userid, productId);


            if (!result.IsSuccess)
            {
                return Fail();

            }

            return Success(result.Data);

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
        public async Task<IActionResult> GetRecommendationProduct([FromQuery] int productId)
        {

            string? userid = UserInfo != null ? UserInfo.UserId : null;
            var result = _productervice.GetRecommendationProduct(userid, productId);

            if (!result.IsSuccess)
            {
                return Fail();
            }

            return Success(result.Data);

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
