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
        /// 合併購物車 - 使用批次更新優化效能
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

                // DTO -> Domain Object
                var domainCartItems = ConvertToDomainCartItems(frontEndCartItems);

                // 如果資料庫沒有購物車，創建新購物車並插入項目
                if (cart == null)
                {
                    cart = await _cartRepository.CreateCartWithItemsAsync(userid, domainCartItems);
                }
                else
                {
                    // 使用富領域模型的合併方法（在記憶體中合併）
                    cart.MergeItems(domainCartItems, productVariants);

                    // 使用批次更新（ExecuteDelete + AddRange）取代 SaveChanges
                    await _cartRepository.UpdateCartItemsBatchAsync(cart.Id, cart.CartItems);
                }

                // 將 productVariants 轉換為字典以便快速查找
                var productVariantDict = productVariants.ToDictionary(pv => pv.Id);
                
                // 將 cartItems 轉換為 DTO 列表
                var productsDTOs = cart.CartItems.ToProductWithCountDTOList(productVariantDict);

                return Success<List<ProductWithCountDTO>>(productsDTOs);
            }
            catch (Exception ex)
            {
                return Error<List<ProductWithCountDTO>>(ex.Message);
            }
        }

        /// <summary>
        /// 以前端數據為主 - 使用批次更新優化效能
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

                // DTO -> Domain Object
                var domainCartItems = ConvertToDomainCartItems(frontEndCartItems);

                // 如果資料庫沒有購物車，創建新購物車並插入項目
                if (cart == null)
                {
                    cart = await _cartRepository.CreateCartWithItemsAsync(userid, domainCartItems);
                }
                else
                {
                    // 使用富領域模型的重建方法（在記憶體中重建）
                    cart.Rebuild(domainCartItems, productVariants);

                    // 使用批次更新（ExecuteDelete + AddRange）取代 SaveChanges
                    await _cartRepository.UpdateCartItemsBatchAsync(cart.Id, cart.CartItems);
                }

                // 將 productVariants 轉換為字典以便快速查找
                var productVariantDict = productVariants.ToDictionary(pv => pv.Id);
                
                // 返回更新後的購物車數據
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
