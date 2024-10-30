using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string GoogleId { get; set; }
        public string NickName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLogin { get; set; }

        public ICollection<FavoriteProducts> FavoriteProducts { get; set; }
        public ICollection<UserShipAddress> UserShipAddresses { get; set; }
    }
}
