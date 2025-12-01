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
        public int CalculateOrderTotal(List<OrderProduct> orderProducts, int shippingPrice)
        {
            int total = 0;

            foreach (var item in orderProducts)
            {
                var price = item.ProductVariant.VariantPrice;
                var discountPrice = GetDiscountAmountForOrderProduct(item);

                price = discountPrice !=0 ? discountPrice: price;
                total= total + price * item.Count;
            }

            return total+ shippingPrice;
        }

        private int GetDiscountAmountForOrderProduct(OrderProduct orderProduct)
        {
            // 確認 OrderProduct 與 ProductVariant 是否存在
            if (orderProduct?.ProductVariant == null)
            {
                return 0;
            }

            // 取得 ProductVariant 的有效折扣
            var currentDate = DateTime.UtcNow;
            var discountAmount = orderProduct.ProductVariant.ProductVariantDiscounts
                .Where(pvd => pvd.Discount.StartDate <= currentDate && pvd.Discount.EndDate >= currentDate)  // 過濾有效期內的折扣
                .Select(pvd => pvd.Discount.DiscountAmount) // 取得折扣金額
                .FirstOrDefault(); // 如果有多個有效折扣，取第一個（可根據需求調整）

            return discountAmount;
        }
    }
}
