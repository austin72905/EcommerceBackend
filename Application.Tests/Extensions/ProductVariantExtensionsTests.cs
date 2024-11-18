using Application.DTOs;
using Application.Extensions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Extensions
{
    [TestFixture]
    public class ProductVariantExtensionsTests
    {
        [Test]
        public void ToProductVariantDTO_ShouldMapVariantToDTO()
        {
            // Arrange
            var productVariant = new ProductVariant
            {
                Id = 1,
                Color = "Red",
                Size = new Size { SizeValue = "M" },
                Stock = 50,
                SKU = "SKU123",
                VariantPrice = 100,
                ProductVariantDiscounts = new List<ProductVariantDiscount>
            {
                new ProductVariantDiscount
                {
                    Discount = new Discount
                    {
                        StartDate = DateTime.Now.AddDays(-1),
                        EndDate = DateTime.Now.AddDays(1),
                        DiscountAmount = 80
                    }
                }
            }
            };

            // Act
            var result = productVariant.ToProductVariantDTO();

            // Assert
            Assert.AreEqual(productVariant.Id, result.VariantID);
            Assert.AreEqual(productVariant.Color, result.Color);
            Assert.AreEqual(productVariant.Size.SizeValue, result.Size);
            Assert.AreEqual(productVariant.Stock, result.Stock);
            Assert.AreEqual(productVariant.SKU, result.SKU);
            Assert.AreEqual(productVariant.VariantPrice, result.Price);
            Assert.AreEqual(80, result.DiscountPrice); // 折扣生效
        }

        [Test]
        public void ToProductVariantDTO_ShouldReturnOriginalPrice_WhenNoDiscounts()
        {
            // Arrange
            var productVariant = new ProductVariant
            {
                Id = 2,
                Color = "Blue",
                Size = new Size { SizeValue = "L" },
                Stock = 30,
                SKU = "SKU456",
                VariantPrice = 200,
                ProductVariantDiscounts = new List<ProductVariantDiscount>() // 無折扣
            };

            // Act
            var result = productVariant.ToProductVariantDTO();

            // Assert
            Assert.AreEqual(productVariant.Id, result.VariantID);
            Assert.AreEqual(productVariant.VariantPrice, result.Price);
            Assert.IsNull(result.DiscountPrice); // 無折扣時為 null
        }

        [Test]
        public void ToProductVariantEntity_ShouldMapDTOToEntity()
        {
            // Arrange
            var variantDTO = new ProductVariantDTO
            {
                VariantID = 1,
                Color = "Green",
                Size = "M",
                Stock = 20,
                SKU = "SKU789",
                Price = 150
            };

            // Act
            var result = variantDTO.ToProductVariantEntity();

            // Assert
            Assert.AreEqual(variantDTO.VariantID, result.Id);
            Assert.AreEqual(variantDTO.Color, result.Color);
            Assert.AreEqual(variantDTO.Stock, result.Stock);
            Assert.AreEqual(variantDTO.SKU, result.SKU);
            Assert.AreEqual(variantDTO.Price, result.VariantPrice);
        }

        [Test]
        public void CalculateDiscountPrice_ShouldReturnMaxDiscount_WhenMultipleValidDiscounts()
        {
            // Arrange
            var productVariant = new ProductVariant
            {
                Id = 1,
                Color="紅",
                Stock= 20,
                SKU="紅-x",
                VariantPrice=20,
                Size=new Size { SizeValue="X"},
                ProductVariantDiscounts = new List<ProductVariantDiscount>
                {
                    new ProductVariantDiscount
                    {
                        Discount = new Discount
                        {
                            StartDate = DateTime.Now.AddDays(-5),
                            EndDate = DateTime.Now.AddDays(5),
                            DiscountAmount = 90
                        }
                    },
                    new ProductVariantDiscount
                    {
                        Discount = new Discount
                        {
                            StartDate = DateTime.Now.AddDays(-1),
                            EndDate = DateTime.Now.AddDays(1),
                            DiscountAmount = 70
                        }
                    }
                }
            };

            // Act
            var result = ProductVariantExtensions.ToProductVariantDTO(productVariant);

            // Assert
            Assert.AreEqual(90, result.DiscountPrice); // 應選擇最大折扣
        }

        [Test]
        public void CalculateDiscountPrice_ShouldReturnNull_WhenNoValidDiscounts()
        {
            // Arrange
            var productVariant = new ProductVariant
            {
                Id = 1,
                Color = "紅",
                Stock = 20,
                SKU = "紅-x",
                VariantPrice = 20,
                Size = new Size { SizeValue = "X" },
                ProductVariantDiscounts = new List<ProductVariantDiscount>
                {
                    
                    
                }
            };
         

            // Act
            var result = ProductVariantExtensions.ToProductVariantDTO(productVariant);

            // Assert
            Assert.IsNull(result.DiscountPrice); // 無有效折扣應返回 null
        }
    }
}
