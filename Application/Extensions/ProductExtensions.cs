﻿using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ProductExtensions
    {
        public static ProductInfomationDTO ToProductInformationDTO(this Product product)
        {
            return new ProductInfomationDTO
            {
                Title = product.Title,
                ProductId = product.Id,
                Price = product.Price,
                DiscountPrice = product.ProductDiscounts?.FirstOrDefault()?.Discount.DiscountAmount ?? product.Price,
                Stock = product.Stock,
                Material = product.Material.Split(',').Select(m => m.Trim()).ToList(),
                Variants = product.ProductVariants?.Select(v => v.ToProductVariantDTO()).ToList(),
                HowToWash = product.HowToWash,
                Features = product.Features,
                Images = product.ProductImages?.Select(i => i.ImageUrl).ToList(),
                CoverImg = product.CoverImg
            };
        }

        public static Product ToProductEntity(this ProductInfomationDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.ProductId,
                Title = productDTO.Title,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                Material = string.Join(", ", productDTO.Material), // 將材料列表轉換為字符串存儲
                HowToWash = productDTO.HowToWash,
                Features = productDTO.Features,
                CoverImg = productDTO.CoverImg,
                ProductVariants = productDTO.Variants?.Select(v => v.ToProductVariantEntity()).ToList()
            };
        }
    }
}
