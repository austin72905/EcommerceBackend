using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Entities
{
    public class Payments
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentKey { get; set; }
        public string PaymentHashIV { get; set; }
        public decimal PaymentAmount { get; set; }
        public byte PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Order Order { get; set; }
    }
}
