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
        /// 合併購物車內容 - 已棄用，請直接使用 Cart.MergeItems()
        /// </summary>
        [Obsolete("此方法已棄用，請直接使用 Cart.MergeItems() 方法")]
        public void MergeCartItems(Cart cart, List<CartItem> frontEndCartItems, IEnumerable<ProductVariant> productVariants)
        {
            // 直接使用富領域模型方法
            cart.MergeItems(frontEndCartItems, productVariants);
        }



        /// <summary>
        /// 清空並重建購物車 - 已棄用，請直接使用 Cart.Rebuild()
        /// </summary>
        [Obsolete("此方法已棄用，請直接使用 Cart.Rebuild() 方法")]
        public void ClearAndRebuildCart(Cart cart, List<CartItem> frontEndCartItems, IEnumerable<ProductVariant> productVariants)
        {
            // 直接使用富領域模型方法
            cart.Rebuild(frontEndCartItems, productVariants);
        }
    }
}
