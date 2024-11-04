
using Application;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productervice;
        private readonly IRedisService _redisService;

        // 是否已驗證
        //private bool IsAuthenticated => HttpContext.Items.ContainsKey("IsAuthenticated") && Convert.ToBoolean(HttpContext.Items["IsAuthenticated"]);


        public ProductController(IProductService productService, IRedisService redisService)
        {
            _productervice = productService;
            _redisService = redisService;
        }

        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList([FromQuery] Filter filter)
        {


            if (string.IsNullOrEmpty(filter.tag) && string.IsNullOrEmpty(filter.kind))
            {
                return Fail("請求類型不得為空");
            }

            int userid = UserInfo != null ? UserInfo.UserId : 0;

            ServiceResult<ProductListResponse> result;

            if(userid == 0)
            {
                //未登錄
                result =await _productervice.GetProducts(filter.kind, filter.tag);
            }
            else
            {
                result =await _productervice.GetProductsForUser(userid, filter.kind, filter.tag);
            }


            

            if (!result.IsSuccess)
            {
                return Fail();
            }


            return Success(result.Data);


        }



        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] int productId)
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            ServiceResult<ProductResponse> result;
            if (userid == 0)
            {
                result =await _productervice.GetProductById(productId);
            }
            else
            {
                result =await _productervice.GetProductByIdForUser(userid, productId);
            }

 

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

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            var result =await _productervice.GetRecommendationProduct(userid, productId);

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
