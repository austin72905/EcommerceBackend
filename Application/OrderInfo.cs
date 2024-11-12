using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class OrderInfo
    {
        public int UserId { get; set; }

        /// <summary>
        /// 訂單產品項目列表
        /// </summary>
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        /// <summary>
        /// 訂單運費
        /// </summary>
        public decimal ShippingFee { get; set; }
        /// <summary>
        /// 訂單地址 (超商地址)
        /// </summary>
        public string ShippingAddress { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        ///  收件門市
        /// </summary>

        public string RecieveStore { get; set; }


        public string RecieveWay { get; set; }

        /// <summary>
        /// 收件人電話
        /// </summary>
        public string ReceiverPhone { get; set; }

        public string Email { get; set; }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
    }

}
