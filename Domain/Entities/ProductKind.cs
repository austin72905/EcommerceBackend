using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductKind
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int KindId { get; set; }

        // 導航屬性
        public Product Product { get; set; }
        public Kind Kind { get; set; }
    }
}
