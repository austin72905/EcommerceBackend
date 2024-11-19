using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICartDomainService
    {
        public void MergeCartItems(Cart cart, List<CartItem> frontEndCartItems, IEnumerable<ProductVariant> productVariants);


        /// <summary>
        /// 清空並重建購物車
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="frontEndCartItems"></param>
        /// <param name="productVariants"></param>
        public void ClearAndRebuildCart(Cart cart, List<CartItem> frontEndCartItems, IEnumerable<ProductVariant> productVariants);
    }
}
