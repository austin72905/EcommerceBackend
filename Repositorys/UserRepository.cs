using EcommerceBackend.Interfaces.Repositorys;

namespace EcommerceBackend.Repositorys
{
    public class UserRepository: IUserRepository
    {
        public UserRepository() 
        { 
            
        }

        public IEnumerable<string> GetFavoriteProductIdsByUser(string userId)
        {
            // UserFavorites 表
            return new List<string>() { "26790367", "2", "3" };

        }
    }
}
