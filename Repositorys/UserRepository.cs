using EcommerceBackend.Interfaces.Repositorys;
using EcommerceBackend.Models;

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

        public UserInfoDTO GetUserInfo(string userid)
        {
            return new UserInfoDTO
            {
                UserId = userid,
                Username="linda",
                Email="abc.@pornmail.com",
                NickName="金牌殺手",
                PhoneNumber="4512367",
                Gender="男",
                Birthday= DateTime.Today,
                ShipAddress=new List<UserShipAddressDTO> 
                { 
                    new UserShipAddressDTO
                    {
                        RecipientName="王大明",
                        PhoneNumber="15459",
                        Email="abc.@pornmail.com",
                        AddressLine="台中市中區大名路377號",
                        IsDefault=true,

                    },
                    new UserShipAddressDTO
                    {
                        RecipientName="王大明",
                        PhoneNumber="15459",
                        Email="abc.@pornmail.com",
                        AddressLine="台中市中區大名路377號",
                        

                    },
                    new UserShipAddressDTO
                    {
                        RecipientName="王大明",
                        PhoneNumber="15459",
                        Email="abc.@pornmail.com",
                        AddressLine="台中市中區大名路377號",
                   
                    }
                }
            };

        }
    }
}
