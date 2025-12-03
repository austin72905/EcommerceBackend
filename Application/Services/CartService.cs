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
        /// 合併購物車 - 使用富領域模型方法
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

                // 如果資料庫沒有購物車，使用富領域模型的工廠方法創建
                if (cart == null)
                {
                    cart = Cart.CreateForUser(userid);
                    await _cartRepository.AddAsync(cart);
                }

                // DTO -> Domain Object（使用富領域模型的工廠方法）
                var domainCartItems = ConvertToDomainCartItems(frontEndCartItems);

                // 使用富領域模型的合併方法
                cart.MergeItems(domainCartItems, productVariants);

                // 保存更改到資料庫
                await _cartRepository.SaveChangesAsync();

                // 優化：使用已載入的 productVariants 資料轉換為 DTO，避免重新載入購物車
                // 將 productVariants 轉換為字典以便快速查找
                var productVariantDict = productVariants.ToDictionary(pv => pv.Id);
                
                // 將 cartItems 轉換為 DTO 列表（使用已載入的資料）
                var productsDTOs = cart.CartItems.ToProductWithCountDTOList(productVariantDict);

                return Success<List<ProductWithCountDTO>>(productsDTOs);
            }
            catch (Exception ex)
            {
                return Error<List<ProductWithCountDTO>>(ex.Message);
            }
        }

        /// <summary>
        /// 以前端數據為主 - 使用富領域模型方法
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

                // 如果資料庫沒有購物車，使用富領域模型的工廠方法創建
                if (cart == null)
                {
                    cart = Cart.CreateForUser(userid);
                    await _cartRepository.AddAsync(cart);
                }

                // DTO -> Domain Object（使用富領域模型的工廠方法）
                var domainCartItems = ConvertToDomainCartItems(frontEndCartItems);

                // 使用富領域模型的重建方法
                cart.Rebuild(domainCartItems, productVariants);

                // 保存更改到資料庫
                await _cartRepository.SaveChangesAsync();

                // 優化：使用已載入的 productVariants 資料轉換為 DTO，避免重新載入購物車
                // 將 productVariants 轉換為字典以便快速查找
                var productVariantDict = productVariants.ToDictionary(pv => pv.Id);
                
                // 返回更新後的購物車數據（使用已載入的資料）
                var productsDTOs = cart.CartItems.ToProductWithCountDTOList(productVariantDict);

                return Success<List<ProductWithCountDTO>>(productsDTOs);
            }
            catch (Exception ex)
            {
                return Error<List<ProductWithCountDTO>>(ex.Message);
            }
        }


        /// <summary>
        /// 應用層轉換成 Domain Object - 使用富領域模型的工廠方法
        /// </summary>
        /// <param name="frontEndCartItems"></param>
        /// <returns></returns>
        private List<CartItem> ConvertToDomainCartItems(List<CartItemDTO> frontEndCartItems)
        {
            return frontEndCartItems.Select(dto => 
                CartItem.Create(dto.ProductVariantId, dto.Quantity)
            ).ToList();
        }

    }
}
