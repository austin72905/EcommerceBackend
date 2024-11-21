﻿using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        Price = op.ProductVariant.VariantPrice
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
                OrderStepInfomation = order.OrderSteps.Select(oStep => new OrderStepDTO
                {
                    Status = (OrderStepStatus)oStep.StepStatus,
                    UpdatedAt = oStep.UpdatedAt,
                }).ToList(),
                ShipInfomation = order.Shipments.Select(sp => new ShipmentDTO
                {
                    Status = (ShipmentStatus)sp.ShipmentStatus,
                    UpdatedAt = sp.UpdatedAt,
                }).ToList(),
                Status = (OrderStatus)order.Status,
                PayWay = (PaymentMethod)order.PayWay,
                ShippingPrice = order.ShippingPrice,
                UpdatedAt = DateTime.Now,
            };
        }


        public static List<OrderInfomationDTO> ToOrderDTOList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => order.ToOrderDTO()).ToList();
        }

    }
}