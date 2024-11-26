using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ProductVariantExtensions
    {
        public static ProductVariantDTO ToProductVariantDTO(this ProductVariant variant)
        {
            return new ProductVariantDTO
            {
                VariantID = variant.Id,
                Color = variant.Color,
                Size = variant.Size.SizeValue,
                Stock = variant.Stock,
                SKU = variant.SKU,
                Price = variant.VariantPrice,
                //DiscountPrice = variant.ProductVariantDiscounts?.FirstOrDefault()?.Discount.DiscountAmount ?? variant.VariantPrice
                DiscountPrice= CalculateDiscountPrice(variant)
            };
        }

        public static IEnumerable<ProductVariantDTO> ToProductInformationDTOs(this IEnumerable<ProductVariant> variants)
        {
            return variants.Select(p => p.ToProductVariantDTO());
        }

        public static ProductVariant ToProductVariantEntity(this ProductVariantDTO variantDTO)
        {
            return new ProductVariant
            {
                Id = variantDTO.VariantID,
                Color = variantDTO.Color,
                Stock = variantDTO.Stock,
                SKU = variantDTO.SKU,
                VariantPrice = variantDTO.Price
                // 需要注意: Size 和 Discount 可能需要額外處理
            };
        }

        // 計算折扣價格的函數
        private static int? CalculateDiscountPrice(ProductVariant productVariant)
        {

            if(productVariant.ProductVariantDiscounts == null)
            {
                return null;
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

    }
}
