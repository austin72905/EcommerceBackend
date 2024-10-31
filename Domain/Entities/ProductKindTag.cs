using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public class ProductKindTag
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int KindId { get; set; }
        public int TagId { get; set; }

        public Product Product { get; set; }
        public Kind Kind { get; set; }
        public Tag Tag { get; set; }
    }
}
