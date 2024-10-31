using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataSource.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EcommerceDBContext context) : base(context)
        {
        }

        public IEnumerable<int> GetFavoriteProductIdsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public User? GetUserInfo(int userid)
        {
            return _dbSet
                .Where(u => u.Id == userid)
                .FirstOrDefault();

        }

        public IEnumerable<UserShipAddress> GetUserShippingAddress(int userid)
        {
            return _context.UserShipAddresses
                .Where(us => us.UserId == userid).AsNoTracking();

        }

        public async Task AddUserShippingAddress(int userid, string address)
        {
            await _context.UserShipAddresses.AddAsync(new UserShipAddress());
            await _context.SaveChangesAsync();
        }

        public async Task ModifyUserShippingAddress(int userid, string address)
        {
            await _context.UserShipAddresses
                    .Where(us => us.UserId == userid && us.Id == 1)
                    .ExecuteUpdateAsync(set => set
                    .SetProperty(prop => prop.RecipientName, address)
                    .SetProperty(prop => prop.UpdatedAt, DateTime.Now));
        }

        public async Task DeleteUserShippingAddress(int userid, int addressId)
        {
            await _context.UserShipAddresses.Where(us => us.UserId == userid && us.Id == addressId).ExecuteDeleteAsync();

        }
    }
}
