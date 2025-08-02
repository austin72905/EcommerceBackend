using Application.DTOs;
using Domain.Entities;

namespace Application.Extensions
{
    public static class CartItemExtensions
    {
        public static CartItemDTO ToCartItemDTO(this CartItem cartItem)
        {
            return new CartItemDTO
            {
                //Id = cartItem.Id,
                //CartId = cartItem.CartId,
                ProductVariantId = cartItem.ProductVariantId,
                Quantity = cartItem.Quantity,
                // 假設 CartItem 中的 ProductVariant 導航屬性已載入
                //ProductName = cartItem.ProductVariant?.Name,
                //ProductPrice = cartItem.ProductVariant?.Price ?? 0 // 若 Price 為 null 則使用 0
            };
        }

        // 如果需要批量轉換，也可以實現一個 IEnumerable 的擴充方法
        public static IEnumerable<CartItemDTO> ToCartItemDTOList(this IEnumerable<CartItem> cartItems)
        {
            return cartItems.Select(cartItem => cartItem.ToCartItemDTO()).ToList();
        }


        public static ProductWithCountDTO ToProductWithCountDTO(this CartItem cartItem)
        {
            return new ProductWithCountDTO
            {
                Product = new ProductInfomationDTO
                {
                    ProductId = cartItem.ProductVariant.Product.Id,
                    Title = cartItem.ProductVariant.Product.Title,
                    //Price = cartItem.ProductVariant.Product.Price,
                    CoverImg = cartItem.ProductVariant.Product.CoverImg,
                    // 填寫其他產品信息
                },
                Count = cartItem.Quantity,
                SelectedVariant = new ProductVariantDTO
                {
                    VariantID = cartItem.ProductVariantId,
                    Color = cartItem.ProductVariant.Color,
                    Size = cartItem.ProductVariant.Size.SizeValue,
                    Price = cartItem.ProductVariant.VariantPrice,
                    Stock = cartItem.ProductVariant.Stock,
                    SKU = cartItem.ProductVariant.SKU,
                    DiscountPrice = CalculateDiscountPrice(cartItem.ProductVariant)

                    // 填寫其他變體信息
                }
            };
        }


        // 計算折扣價格的函數
        private static int? CalculateDiscountPrice(ProductVariant productVariant)
        {
            if (productVariant?.ProductVariantDiscounts == null)
            {
                // 處理為 null 的情況，可以選擇返回 null 或原價
                return null; // 或 return productVariant.OriginalPrice;
            }

            // 查找當前有效的折扣
            var currentDiscount = productVariant.ProductVariantDiscounts
                .Where(pvd => pvd.Discount.StartDate <= DateTime.Now && pvd.Discount.EndDate >= DateTime.Now)
                .OrderByDescending(pvd => pvd.Discount.DiscountAmount) // 若有多個折扣，選擇最大折扣
                .FirstOrDefault();

            // 計算並返回折扣價格，如果沒有有效折扣則返回原價
            return currentDiscount != null
                ? currentDiscount.Discount.DiscountAmount
                : (int?)null; // 若無折扣，則返回 null 或可視為原價
        }

        // 批量轉換擴充方法
        public static List<ProductWithCountDTO> ToProductWithCountDTOList(this IEnumerable<CartItem> cartItems)
        {
            return cartItems.Select(cartItem => cartItem.ToProductWithCountDTO()).ToList();
        }
    }
}
