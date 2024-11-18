using Application.DTOs;
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
                Material = product.Material.Split(',').Select(m => m.Trim()).ToList(),
                Variants = product.ProductVariants?.Select(v => v.ToProductVariantDTO()).ToList(),              
                HowToWash = product.HowToWash,
                Features = product.Features,
                Images = product.ProductImages?.Select(i => i.ImageUrl).ToList(),
                CoverImg = product.CoverImg
            };


            
        }

        public static IEnumerable<ProductInfomationDTO> ToProductInformationDTOs(this IEnumerable<Product> products)
        {
            return products.Select(p => p.ToProductInformationDTO());
        }

        public static Product ToProductEntity(this ProductInfomationDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.ProductId,
                Title = productDTO.Title,
                Material = string.Join(", ", productDTO.Material), // 將材料列表轉換為字符串存儲
                HowToWash = productDTO.HowToWash,
                Features = productDTO.Features,
                CoverImg = productDTO.CoverImg,
                ProductVariants = productDTO.Variants?.Select(v => v.ToProductVariantEntity()).ToList()
            };
        }
    }
}
