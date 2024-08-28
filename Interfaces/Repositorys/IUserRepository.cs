namespace EcommerceBackend.Interfaces.Repositorys
{
    public interface IUserRepository
    {
        IEnumerable<string> GetFavoriteProductIdsByUser(string userId);
    }
}
