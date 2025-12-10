using Application.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Extensions
{
    public static class OrderExtensions
    {
        public static OrderInfomationDTO ToOrderDTO(this Order order)
        {
            return new OrderInfomationDTO
            {
                Id = order.Id,
                ProductList = order.OrderProducts.Select(op => new ProductWithCountDTO
                {
                    Count = op.Count,
                    Product = new ProductInfomationDTO
                    {
                        Title = op.ProductVariant.Product.Title,
                        ProductId = op.ProductVariant.ProductId,
                        CoverImg = op.ProductVariant.Product.CoverImg
                    },
                    SelectedVariant = new ProductVariantDTO
                    {
                        VariantID = op.ProductVariant.Id,
                        Color = op.ProductVariant.Color,
                        Size = op.ProductVariant.Size.SizeValue,
                        SKU = op.ProductVariant.SKU,
                        Stock = op.ProductVariant.Stock,
                        Price = op.ProductVariant.VariantPrice,
                        DiscountPrice = CalculateDiscountPrice(op.ProductVariant)
                    }
                }).ToList(),

                RecordCode = order.RecordCode,
                OrderPrice = order.OrderPrice,
                Address = new OrderAddressDTO
                {
                    Receiver = order.Receiver,
                    PhoneNumber = order.PhoneNumber,
                    ShippingAddress = order.ShippingAddress,
                },
                ShipInfomation = order.Shipments.Select(sp => new ShipmentDTO
                {
                    Status = (ShipmentStatus)sp.ShipmentStatus,
                    UpdatedAt = sp.UpdatedAt,
                }).ToList(),
                Status = (OrderStatus)order.Status,
                PayWay = (PaymentMethod)order.PayWay,
                ShippingPrice = order.ShippingPrice,
                UpdatedAt = order.UpdatedAt, // 使用訂單實際的更新時間
                
                // ============ 關鍵狀態時間戳（優先使用，提升查詢性能） ============
                PaidAt = order.PaidAt,
                ShippedAt = order.ShippedAt,
                PickedUpAt = order.PickedUpAt,
                CompletedAt = order.CompletedAt,
            };
        }


        public static List<OrderInfomationDTO> ToOrderDTOList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => order.ToOrderDTO()).ToList();
        }


        private static int? CalculateDiscountPrice(ProductVariant productVariant)
        {

            if (productVariant.ProductVariantDiscounts == null)
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
