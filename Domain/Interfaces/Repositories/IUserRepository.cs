using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<int> GetFavoriteProductIdsByUser(int userId);
        public User? GetUserInfo(int userid);

        public IEnumerable<UserShipAddress> GetUserShippingAddress(int userid);

        public Task AddUserShippingAddress(int userid, string address);

        public Task ModifyUserShippingAddress(int userid, string address);

        public Task DeleteUserShippingAddress(int userid, int addressId);
    }
}
