using Application.DTOs;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Application.Services
{
    public class CartService : BaseService<CartService>, ICartService
    {
        private readonly ICartDomainService _cartDomainService;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        public CartService
            (
                ICartDomainService cartDomainService,
                IProductRepository productRepository,
                IUserRepository userRepository,
                ICartRepository cartRepository,
                ILogger<CartService> logger
            ) : base(logger)
        {
            _cartDomainService = cartDomainService;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }



        /// <summary>
        /// 合併購物車
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="frontEndCartItems"></param>
        /// <param name="isCover">以前端數據為主</param>
        /// <returns></returns>
        public async Task<ServiceResult<List<ProductWithCountDTO>>> MergeCartContent(int userid, List<CartItemDTO> frontEndCartItems, bool isCover = false)
        {
            try
            {
                if (isCover)
                {
                    return await MergeCartContentCover(userid, frontEndCartItems);
                }


                var cart = await _cartRepository.GetCartByUserId(userid);

                var productVariants = await _productRepository.GetProductVariants(frontEndCartItems.Select(fc => fc.ProductVariantId));

                // 如果資料庫沒有購物車
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userid,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CartItems = new List<CartItem>()
                    };

                    await _cartRepository.AddAsync(cart);

                }


                // 合併前端傳來的購物車項目和資料庫中的購物車項目
                //foreach (var frontEndItem in frontEndCartItems)
                //{
                //    var existingItem = cart.CartItems
                //        .FirstOrDefault(ci => ci.ProductVariantId == frontEndItem.ProductVariantId);

                //    if (existingItem != null)
                //    {
                //        // 如果已存在，則更新數量
                //        int num = Math.Max(existingItem.Quantity, frontEndItem.Quantity);
                //        //取大
                //        existingItem.Quantity = num;


                //    }
                //    else
                //    {
                //        // 如果不存在，則新增一個新的購物車項目
                //        cart.CartItems.Add(new CartItem
                //        {

                //            ProductVariant = productVariants.FirstOrDefault(ci => ci.Id == frontEndItem.ProductVariantId),
                //            ProductVariantId = frontEndItem.ProductVariantId,
                //            Quantity = frontEndItem.Quantity
                //        });
                //    }
                //}


                //// 更新購物車的最後更新時間
                //cart.UpdatedAt = DateTime.Now;


                // DTO -> Domain Object
                var domainCartItems = ConvertToDomainCartItems(frontEndCartItems);

                _cartDomainService.MergeCartItems(cart, domainCartItems, productVariants);

                // 保存更改到資料庫
                await _cartRepository.SaveChangesAsync();


                // 將 cartItems 轉換為 DTO 列表
                var productsDTOs = cart.CartItems.ToProductWithCountDTOList();

                return Success<List<ProductWithCountDTO>>(productsDTOs);

            }
            catch (Exception ex)
            {
                return Error<List<ProductWithCountDTO>>(ex.Message);

            }



        }

        /// <summary>
        /// 以前端數據為主
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="frontEndCartItems"></param>
        /// <returns></returns>

        public async Task<ServiceResult<List<ProductWithCountDTO>>> MergeCartContentCover(int userid, List<CartItemDTO> frontEndCartItems)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserId(userid);

                var productVariants = await _productRepository.GetProductVariants(frontEndCartItems.Select(fc => fc.ProductVariantId));

                // 如果資料庫沒有購物車
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userid,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CartItems = new List<CartItem>()
                    };

                    await _cartRepository.AddAsync(cart);
                }


                var domainCartItems = ConvertToDomainCartItems(frontEndCartItems);

                //// 清空當前購物車中的所有項目，以完全採用前端的購物車數據
                //cart.CartItems.Clear();

                //// 根據前端提供的項目添加到購物車中
                //foreach (var frontEndItem in frontEndCartItems)
                //{
                //    // 從產品變體中查找對應的產品
                //    var productVariant = productVariants.FirstOrDefault(ci => ci.Id == frontEndItem.ProductVariantId);

                //    if (productVariant != null)
                //    {
                //        // 將前端項目新增到購物車中
                //        cart.CartItems.Add(new CartItem
                //        {
                //            ProductVariant = productVariant,
                //            ProductVariantId = frontEndItem.ProductVariantId,
                //            Quantity = frontEndItem.Quantity
                //        });
                //    }
                //}

                //// 更新購物車的最後更新時間
                //cart.UpdatedAt = DateTime.Now;

                // 調用 Domain Service 處理購物車清空與重建
                _cartDomainService.ClearAndRebuildCart(cart, domainCartItems, productVariants);

                // 保存更改到資料庫
                await _cartRepository.SaveChangesAsync();

                // 返回更新後的購物車數據
                var productsDTOs = cart.CartItems.ToProductWithCountDTOList();

                return Success<List<ProductWithCountDTO>>(productsDTOs);
               
            }
            catch (Exception ex)
            {

                return Error<List<ProductWithCountDTO>>(ex.Message);
                
            }


        }


        /// <summary>
        /// 應用層轉換成 Domain Object 
        /// </summary>
        /// <param name="frontEndCartItems"></param>
        /// <returns></returns>
        private List<CartItem> ConvertToDomainCartItems(List<CartItemDTO> frontEndCartItems)
        {
            return frontEndCartItems.Select(dto => new CartItem
            {
                ProductVariantId = dto.ProductVariantId,
                Quantity = dto.Quantity
            }).ToList();
        }

    }
}
