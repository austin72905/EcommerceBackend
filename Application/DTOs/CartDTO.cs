using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();

        /// <summary>
        /// 是否用前端數據覆蓋資料庫數據
        /// </summary>
        public bool IsCover { get; set; }
    }
    public class CartItemDTO
    {
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
    }
}
