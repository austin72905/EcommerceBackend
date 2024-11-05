using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<int>> GetFavoriteProductIdsByUser(int userId);
        public Task<User?> GetUserInfo(int userid);

        public User? CheckUserExists(string userName);

        public Task<User?> GetUserIfExistsByGoogleID(string gooleID);

        public Task AddUser(User user);

        public IEnumerable<UserShipAddress> GetUserShippingAddress(int userid);

        public Task AddUserShippingAddress(int userid, string address);

        public Task ModifyUserShippingAddress(int userid, string address);

        public Task DeleteUserShippingAddress(int userid, int addressId);

        public Task RemoveFromFavoriteList(int userid, int productId);

        public Task AddToFavoriteList(int userid,int productId);
    }
}
