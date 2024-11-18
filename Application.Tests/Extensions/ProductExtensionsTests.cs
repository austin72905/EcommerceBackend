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
    public class ProductExtensionsTests
    {
        [Test]
        public void ToProductInformationDTO_ShouldMapProductToDTO()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Title = "Test Product",
                Material = "Cotton, Polyester",
                HowToWash = "Hand wash only",
                Features = "Lightweight, Durable",
                CoverImg = "cover.jpg",
                ProductVariants = new List<ProductVariant>
                {
                    new ProductVariant 
                    { 
                        Id = 1,
                        Color = "紅",
                        Stock = 20,
                        SKU = "紅-x",
                        VariantPrice = 20,
                        Size = new Size { SizeValue = "X" },
                        ProductVariantDiscounts=new List<ProductVariantDiscount>()
                        {
                            new ProductVariantDiscount()
                            {
                                Discount=new Discount()
                                {
                                    DiscountAmount=100,
                                    StartDate=DateTime.Now,
                                    CreatedAt=DateTime.Now,
                                }
                            }
                        }
                    },
                    new ProductVariant 
                    { 
                        Id = 2,
                        Color = "綠",
                        Stock = 20,
                        SKU = "綠-x",
                        VariantPrice = 20,
                        Size = new Size { SizeValue = "X" },
                        ProductVariantDiscounts=new List<ProductVariantDiscount>()
                        {
                            new ProductVariantDiscount()
                            {
                                Discount=new Discount()
                                {
                                    DiscountAmount=100,
                                    StartDate=DateTime.Now,
                                    CreatedAt=DateTime.Now,
                                }
                            }
                        }
                    }
                },
                ProductImages = new List<ProductImage>
                {
                    new ProductImage { ImageUrl = "img1.jpg" },
                    new ProductImage { ImageUrl = "img2.jpg" }
                }
            };

            // Act
            var result = product.ToProductInformationDTO();

            // Assert
            Assert.AreEqual(product.Title, result.Title);
            Assert.AreEqual(product.Id, result.ProductId);
            CollectionAssert.AreEqual(new List<string> { "Cotton", "Polyester" }, result.Material);
            CollectionAssert.AreEqual(new List<string> { "img1.jpg", "img2.jpg" }, result.Images);
            Assert.AreEqual(product.HowToWash, result.HowToWash);
            Assert.AreEqual(product.Features, result.Features);
            Assert.AreEqual(product.CoverImg, result.CoverImg);
            Assert.AreEqual(2, result.Variants.Count);
        }

        [Test]
        public void ToProductInformationDTOs_ShouldMapMultipleProductsToDTOs()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = 1, Title = "Product 1", Material = "Cotton" },
            new Product { Id = 2, Title = "Product 2", Material = "Polyester" }
        };

            // Act
            var result = products.ToProductInformationDTOs().ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Product 1", result[0].Title);
            Assert.AreEqual("Product 2", result[1].Title);
            CollectionAssert.AreEqual(new List<string> { "Cotton" }, result[0].Material);
            CollectionAssert.AreEqual(new List<string> { "Polyester" }, result[1].Material);
        }

        [Test]
        public void ToProductEntity_ShouldMapDTOToProduct()
        {
            // Arrange
            var productDTO = new ProductInfomationDTO
            {
                ProductId = 1,
                Title = "Test Product",
                Material = new List<string> { "Cotton", "Polyester" },
                HowToWash = "Hand wash only",
                Features = "Lightweight, Durable",
                CoverImg = "cover.jpg",
                Variants = new List<ProductVariantDTO>
            {
                new ProductVariantDTO { VariantID = 1, },
                new ProductVariantDTO { VariantID = 2}
            }
            };

            // Act
            var result = productDTO.ToProductEntity();

            // Assert
            Assert.AreEqual(productDTO.ProductId, result.Id);
            Assert.AreEqual(productDTO.Title, result.Title);
            Assert.AreEqual("Cotton, Polyester", result.Material);
            Assert.AreEqual(productDTO.HowToWash, result.HowToWash);
            Assert.AreEqual(productDTO.Features, result.Features);
            Assert.AreEqual(productDTO.CoverImg, result.CoverImg);
            Assert.AreEqual(2, result.ProductVariants.Count);
        }
    }
}
