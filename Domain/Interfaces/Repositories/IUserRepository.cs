using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<int> GetFavoriteProductIdsByUser(string userId);
        public User? GetUserInfo(string userid);

        public IEnumerable<UserShipAddress> GetUserShippingAddress(string userid);

        public string AddUserShippingAddress(string userid, string address);

        public string ModifyUserShippingAddress(string userid, string address);

        public string DeleteUserShippingAddress(string userid, int addressId);
    }
}
