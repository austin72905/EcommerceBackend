using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        public async Task AddUserShippingAddress(int userid, UserShipAddress address)
        {
            await _context.UserShipAddresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task ModifyUserShippingAddress(int userid, UserShipAddress address)
        {
            await _context.UserShipAddresses
                    .Where(ua => ua.UserId == userid && ua.Id == address.Id)
                    .ExecuteUpdateAsync(set => set
                    .SetProperty(prop => prop.RecipientName, address.RecipientName)
                    .SetProperty(prop => prop.RecieveStore, address.RecieveStore)
                    .SetProperty(prop => prop.PhoneNumber, address.PhoneNumber)
                    .SetProperty(prop => prop.RecieveWay, address.RecieveWay)
                    .SetProperty(prop => prop.AddressLine, address.AddressLine)
                    .SetProperty(prop => prop.UpdatedAt, DateTime.Now));
        }

        public async Task DeleteUserShippingAddress(int userid, int addressId)
        {
            await _context.UserShipAddresses.Where(us => us.UserId == userid && us.Id == addressId).ExecuteDeleteAsync();

        }

        /// <summary>
        /// 設定為預設的常用地址
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public async Task SetDefaultShippingAddress(int userid, int addressId)
        {
            // 取得該用戶的所有地址
            var addresses = await _context.UserShipAddresses
                                           .Where(a => a.UserId == userid)
                                           .AsTracking()
                                           .ToListAsync();

            // 將所有地址的 IsDefault 設為 false
            foreach (var address in addresses)
            {
                address.IsDefault = false;
            }

            // 將指定的地址設為默認
            var defaultAddress = addresses.FirstOrDefault(a => a.Id == addressId);
            if (defaultAddress != null)
            {
                defaultAddress.IsDefault = true;
            }

            // 儲存更改
            await _context.SaveChangesAsync();
        }

        public User? CheckUserExists(string userName)
        {
            return _dbSet.Where(u => u.Username== userName).FirstOrDefault();
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
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

       

        public async Task ModifyUserInfo(User user)
        {
            await _dbSet
                    .Where(u => u.Id == user.Id )
                    .ExecuteUpdateAsync(set => set
                    .SetProperty(prop => prop.NickName, user.NickName)
                    .SetProperty(prop => prop.Gender, user.Gender)
                    .SetProperty(prop => prop.PhoneNumber, user.PhoneNumber)
                    .SetProperty(prop => prop.Birthday, user.Birthday)
                    .SetProperty(prop => prop.UpdatedAt, DateTime.Now));
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
