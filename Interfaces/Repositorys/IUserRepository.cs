using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface IUserRepository
    {
        public IEnumerable<string> GetFavoriteProductIdsByUser(string userId);

        public UserInfoDTO GetUserInfo(string userid);
    }
}
