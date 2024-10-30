using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Entities
{
    public class ProductVariantDiscount
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public int DiscountId { get; set; }

        public ProductVariant ProductVariant { get; set; }
        public Discount Discount { get; set; }
    }
}
