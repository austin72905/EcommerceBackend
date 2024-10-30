﻿using EcommerceBackend.Models;
using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IOrderService
    {
        public ServiceResult<List<OrderInfomationDTO>> GetOrders(string userid);

        public ServiceResult<OrderInfomationDTO> GetOrderInfo(string userid,string recordCode);


        public ServiceResult<PaymentRequestDataWithUrl> GenerateOrder();



    }
}