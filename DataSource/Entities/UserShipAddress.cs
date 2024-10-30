using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Entities
{
    public class UserShipAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RecipientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AddressLine { get; set; }
        public bool IsDefault { get; set; }

        public User User { get; set; }
    }
}
