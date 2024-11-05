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

        public async Task<IEnumerable<int>> GetFavoriteProductIdsByUser(int userId)
        {
            // UserFavorites 表
            //return new List<int>() { 26790367, 2, 3 };

            return await _context.FavoriteProducts.
                Where(x => x.UserId == userId)
                .Select(x => x.ProductId)
                .ToListAsync();
        }

        public async Task<User?> GetUserInfo(int userid)
        {
            return await _dbSet
                .Where(u => u.Id == userid)
                .FirstOrDefaultAsync();

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

        public User? CheckUserExists(string userName)
        {
            return _dbSet.Where(u => u.Username== userName).FirstOrDefault();
        }

        

        public async Task<User?> GetUserIfExistsByGoogleID(string gooleID)
        {
            return await _dbSet.Where(u => u.GoogleId == gooleID).FirstOrDefaultAsync();
        }

        public async Task AddUser(User user)
        {
            await AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task RemoveFromFavoriteList(int userid, int productId)
        {
            await _context.FavoriteProducts.Where(fp => fp.UserId == userid && fp.ProductId == productId).ExecuteDeleteAsync();
        }

        public async Task AddToFavoriteList(int userid, int productId)
        {
            var alreadyfavorited = await _context.FavoriteProducts
                .AnyAsync(fp => fp.UserId == userid && fp.ProductId == productId);

            if (alreadyfavorited)
            {
                return;
            }

            var favoriteProduct = new FavoriteProduct 
            { 
                ProductId = productId,
                UserId=userid ,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            
            };
            await _context.FavoriteProducts.AddAsync(favoriteProduct);
            await SaveChangesAsync();
        }
    }
}
