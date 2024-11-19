using Domain.Entities;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CartDomainService: ICartDomainService
    {
        /// <summary>
        /// 合併購物車內容
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="frontEndCartItems"></param>
        /// <param name="productVariants"></param>
        public void MergeCartItems(Cart cart, List<CartItem> frontEndCartItems, IEnumerable<ProductVariant> productVariants)
        {
            foreach (var frontEndItem in frontEndCartItems)
            {
                var existingItem = cart.CartItems
                    .FirstOrDefault(ci => ci.ProductVariantId == frontEndItem.ProductVariantId);

                if (existingItem != null)
                {
                    // 如果已存在，則更新數量
                    existingItem.Quantity = Math.Max(existingItem.Quantity, frontEndItem.Quantity);
                }
                else
                {
                    // 如果不存在，則新增一個新的購物車項目
                    cart.CartItems.Add(new CartItem
                    {
                        ProductVariant = productVariants.FirstOrDefault(ci => ci.Id == frontEndItem.ProductVariantId),
                        ProductVariantId = frontEndItem.ProductVariantId,
                        Quantity = frontEndItem.Quantity
                    });
                }
            }

            // 更新購物車的最後更新時間
            cart.UpdatedAt = DateTime.Now;
        }



        /// <summary>
        /// 清空並重建購物車
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="frontEndCartItems"></param>
        /// <param name="productVariants"></param>
        public void ClearAndRebuildCart(Cart cart, List<CartItem> frontEndCartItems, IEnumerable<ProductVariant> productVariants)
        {
            // 清空當前購物車
            cart.CartItems.Clear();

            // 根據前端數據重新添加項目
            foreach (var frontEndItem in frontEndCartItems)
            {
                var productVariant = productVariants.FirstOrDefault(ci => ci.Id == frontEndItem.ProductVariantId);

                if (productVariant != null)
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ProductVariant = productVariant,
                        ProductVariantId = frontEndItem.ProductVariantId,
                        Quantity = frontEndItem.Quantity
                    });
                }
            }

            // 更新購物車的最後更新時間
            cart.UpdatedAt = DateTime.Now;
        }
    }
}
