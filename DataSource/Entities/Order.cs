using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecordCode { get; set; }
        public int? AddressId { get; set; }
        public string Receiver { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public int OrderPrice { get; set; }
        public int Status { get; set; }
        public int PayWay { get; set; }
        public int ShippingPrice { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public UserShipAddress Address { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public ICollection<OrderStep> OrderSteps { get; set; }
    }
}
