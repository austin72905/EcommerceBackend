using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace DataSource.Repositories
{
    public class UserRepository: IUserRepository
    {
        public IEnumerable<int> GetFavoriteProductIdsByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public User? GetUserInfo(string userid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserShipAddress> GetUserShippingAddress(string userid)
        {
            throw new NotImplementedException();
        }

        public string AddUserShippingAddress(string userid, string address)
        {
            throw new NotImplementedException();
        }

        public string ModifyUserShippingAddress(string userid, string address)
        {
            throw new NotImplementedException();
        }

        public string DeleteUserShippingAddress(string userid, int addressId)
        {
            throw new NotImplementedException();
        }
    }
}
