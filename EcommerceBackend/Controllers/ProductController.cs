
using Application;
using Application.DTOs;
using Application.Interfaces;
using Common.Interfaces.Infrastructure;
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


            if (string.IsNullOrEmpty(filter.tag) && string.IsNullOrEmpty(filter.kind) && string.IsNullOrEmpty(filter.query))
            {
                return Fail("請求類型不得為空");
            }

            int userid = UserInfo != null ? UserInfo.UserId : 0;

            ServiceResult<List<ProductWithFavoriteStatusDTO>> result;

            if(userid == 0)
            {
                //未登錄
                result =await _productervice.GetProducts(filter.kind, filter.tag,filter.query);
            }
            else
            {
                result =await _productervice.GetProductsForUser(userid, filter.kind, filter.tag);
            }


            

            if (!result.IsSuccess)
            {
                return Fail(msg:result.ErrorMessage);
            }


            return Success(result.Data);


        }


        [HttpGet("GetProductBasicInfoList")]
        public async Task<IActionResult> GetProductBasicInfoList([FromQuery] Filter filter)
        {


            if (string.IsNullOrEmpty(filter.tag) && string.IsNullOrEmpty(filter.kind) && string.IsNullOrEmpty(filter.query))
            {
                return Fail("請求類型不得為空");
            }

            int userid = UserInfo != null ? UserInfo.UserId : 0;

            var result = await _productervice.GetProductsBasicInfo(filter.kind, filter.tag,filter.query);
            

            if (!result.IsSuccess)
            {
                return Fail(msg: result.ErrorMessage);
            }


            return Success(result.Data);


        }


        [HttpPost("GetProductDynamicInfoList")]
        public async Task<IActionResult> GetProductDynamicInfoList([FromBody] ProductDynamicInfo info)
        {



            int userid = UserInfo != null ? UserInfo.UserId : 0;

            ServiceResult<List<ProductDynamicDTO>> result;
            if (userid == 0)
            {
                //未登錄
                result = await _productervice.GetProductsDynamicInfo(info.ProductIdList);
            }
            else
            {
                result = await _productervice.GetProductsDynamicInfoForUser(info.ProductIdList, userid);
            }


            //var result = await _productervice.GetProductsDynamicInfo(info.ProductIdList);


            if (!result.IsSuccess)
            {
                return Fail(msg: result.ErrorMessage);
            }


            return Success(result.Data);


        }



        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] int productId)
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            ServiceResult<ProductWithFavoriteStatusDTO> result;
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



        [HttpGet("GetProductBasicInfoById")]
        public async Task<IActionResult> GetProductBasicInfoById([FromQuery] int productId)
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            
            var result = await _productervice.GetProductBasicInfoById(productId);
           

            if (!result.IsSuccess)
            {
                return Fail(msg: result.ErrorMessage);

            }

            return Success(result.Data);

        }

        [HttpPost("GetProductDynamicInfo")]
        public async Task<IActionResult> GetProductDynamicInfo([FromBody] ProductDynamic info)
        {



            int userid = UserInfo != null ? UserInfo.UserId : 0;




            var result = await _productervice.GetProductDynamicInfoById(info.ProductId);


            if (!result.IsSuccess)
            {
                return Fail(msg: result.ErrorMessage);
            }


            return Success(result.Data);


        }

        /// <summary>
        /// 獲取完整的商品資訊（包含基本資訊和動態資訊）- 整合 API
        /// </summary>
        [HttpGet("GetProductCompleteInfoById")]
        public async Task<IActionResult> GetProductCompleteInfoById([FromQuery] int productId)
        {
            int userId = UserInfo != null ? UserInfo.UserId : 0;

            ServiceResult<ProductCompleteDTO> result;
            if (userId == 0)
            {
                // 未登入用戶
                result = await _productervice.GetProductCompleteInfoById(productId);
            }
            else
            {
                // 已登入用戶
                result = await _productervice.GetProductCompleteInfoByIdForUser(userId, productId);
            }

            if (!result.IsSuccess)
            {
                return Fail(msg: result.ErrorMessage);
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

        [HttpGet("GetRecommendationProductBasicInfo")]
        public async Task<IActionResult> GetRecommendationProductBasicInfo([FromQuery] int productId)
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            var result = await _productervice.GetRecommendationProductBasicInfo(userid, productId);

            if (!result.IsSuccess)
            {
                return Fail();
            }

            return Success(result.Data);

        }

        [HttpGet("GetfavoriteList")]
        public async Task<IActionResult> GetfavoriteList()
        {

            int userid = UserInfo != null ? UserInfo.UserId : 0;
            var result = await _productervice.GetfavoriteList(userid);

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

        public string? query { get; set; }
    }

    public class Product
    {

    }

    public class ProductDynamicInfo
    {
        public List<int> ProductIdList { get; set; }
    }

    public class ProductDynamic
    {
        public int ProductId { get; set; }
    }
}
