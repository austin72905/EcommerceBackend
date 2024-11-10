﻿using Domain.Entities;

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

        public Task AddUserShippingAddress(int userid, UserShipAddress address);

        public Task ModifyUserShippingAddress(int userid, UserShipAddress address);

        public Task DeleteUserShippingAddress(int userid, int addressId);

        /// <summary>
        /// 設定某個常用地址為預設地址
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public Task SetDefaultShippingAddress(int userid, int addressId);

        public Task RemoveFromFavoriteList(int userid, int productId);

        public Task AddToFavoriteList(int userid,int productId);
    }
}
