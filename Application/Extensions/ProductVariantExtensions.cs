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
                DiscountPrice = variant.ProductVariantDiscounts?.FirstOrDefault()?.Discount.DiscountAmount ?? variant.VariantPrice
            };
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

    }
}
