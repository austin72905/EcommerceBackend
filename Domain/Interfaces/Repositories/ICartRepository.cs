using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ICartRepository
    {
        public Cart GetCartByUserId(int userId);


    }
}
