using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<ProductDiscount> ProductDiscounts { get; set; }
        public ICollection<ProductVariantDiscount> ProductVariantDiscounts { get; set; }
    }
}
