using Domain.Entities;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class OrderDomainService : IOrderDomainService
    {
        /// <summary>
        /// 計算訂單總價
        /// 優化：使用 OrderProduct.ProductPrice（已保存的價格）而不是 ProductVariant 導航屬性
        /// </summary>
        public int CalculateOrderTotal(List<OrderProduct> orderProducts, int shippingPrice, Dictionary<int, ProductVariant>? productVariants = null)
        {
            int total = 0;

            foreach (var item in orderProducts)
            {
                // 使用 OrderProduct.ProductPrice（已保存的價格），而不是 ProductVariant.VariantPrice
                // 這樣就不需要依賴 ProductVariant 導航屬性
                var price = item.ProductPrice;
                
                // 如果有提供 productVariants 字典，嘗試計算折扣
                var discountPrice = GetDiscountAmountForOrderProduct(item, productVariants);

                // 如果有折扣，使用折扣價格；否則使用原價
                price = discountPrice != 0 ? discountPrice : price;
                total = total + price * item.Count;
            }

            return total + shippingPrice;
        }

        /// <summary>
        /// 取得訂單商品的折扣金額
        /// 優化：使用提供的 productVariants 字典，而不是依賴 ProductVariant 導航屬性
        /// </summary>
        private int GetDiscountAmountForOrderProduct(OrderProduct orderProduct, Dictionary<int, ProductVariant>? productVariants = null)
        {
            // 如果沒有提供 productVariants 字典，無法計算折扣，返回 0
            if (productVariants == null || !productVariants.ContainsKey(orderProduct.ProductVariantId))
            {
                return 0;
            }

            var productVariant = productVariants[orderProduct.ProductVariantId];
            
            // 確認 ProductVariantDiscounts 是否存在
            if (productVariant?.ProductVariantDiscounts == null)
            {
                return 0;
            }

            // 取得 ProductVariant 的有效折扣
            var currentDate = DateTime.UtcNow;
            var discountAmount = productVariant.ProductVariantDiscounts
                .Where(pvd => pvd.Discount != null && 
                              pvd.Discount.StartDate <= currentDate && 
                              pvd.Discount.EndDate >= currentDate)  // 過濾有效期內的折扣
                .Select(pvd => pvd.Discount.DiscountAmount) // 取得折扣金額
                .FirstOrDefault(); // 如果有多個有效折扣，取第一個（可根據需求調整）

            return discountAmount;
        }
    }
}
