using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IOrderDomainService
    {
        /// <summary>
        /// 計算訂單總價
        /// </summary>
        /// <param name="orderProducts">訂單商品列表</param>
        /// <param name="shippingPrice">運費</param>
        /// <param name="productVariants">可選的商品變體字典，用於折扣計算（key: ProductVariantId, value: ProductVariant）</param>
        public int CalculateOrderTotal(List<OrderProduct> orderProducts, int shippingPrice, Dictionary<int, ProductVariant>? productVariants = null);
    }
}
