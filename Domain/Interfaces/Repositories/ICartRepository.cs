using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ICartRepository
    {
        public Task<Cart?> GetCartByUserId(int userId);


    }
}
