using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TenantConfig
    {
        public int Id { get; set; }
        public string MerchantId { get; set; } 
        public string SecretKey { get; set; } 
        public string HashIV { get; set; } 

        // 導航屬性
        public ICollection<Payment> Payments { get; set; } 
    }
}
