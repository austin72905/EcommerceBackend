﻿using EcommerceBackend.Interfaces.Services;
using EcommerceBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController: BaseController
    {
        private readonly IProductService _productervice;

        // 是否已驗證
        //private bool IsAuthenticated => HttpContext.Items.ContainsKey("IsAuthenticated") && Convert.ToBoolean(HttpContext.Items["IsAuthenticated"]);


        public ProductController(IProductService productService)
        {
            _productervice=productService;
        }

        [HttpGet("GetProductList")]
        public IActionResult GetProductList([FromHeader(Name = "ALP-User-Id")] string? userid,[FromQuery]Filter filter) 
        {


            // 之後可以驗證是否已登錄

          

            if (IsAuthenticated)
            {
                
                return Content($"isAuthenticated :  {IsAuthenticated}");
            }

            if(string.IsNullOrEmpty(filter.tag) && string.IsNullOrEmpty(filter.kind))
            {
                return Content($"no data\n{filter.kind}\n{filter.tag}");
            }


            var products = _productervice.GetProducts(userid, filter.kind, filter.tag);

            
            return Ok(products);
        }



        [HttpGet("GetProductById")]
        public IActionResult GetProductById([FromHeader(Name = "ALP-User-Id")] string? userid, [FromQuery] string productId)
        {
            // 之後可以驗證是否已登錄

            // 可以驗證product 不存在的反回 code msg data
            var product = _productervice.GetProductById(userid, productId);


            return Ok(product);
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
        public IActionResult GetRecommendationProduct([FromHeader(Name = "ALP-User-Id")] string? userid, [FromQuery] string productId)
        {
            // 之後可以驗證是否已登錄

            var products = _productervice.GetRecommendationProduct(userid, productId);

            return Ok(products);
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
