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

        public IEnumerable<UserShipAddressDTO> GetUserShippingAddress(string userid)
        {
            return new List<UserShipAddressDTO>
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
                };

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
                
            };

        }

        public string AddUserShippingAddress(string userid, UserShipAddressDTO address)
        {
            return "ok,add address success";
        }

        public string DeleteUserShippingAddress(string userid, int addressId)
        {
            return "ok,delete address success";
        }

        public string ModifyUserShippingAddress(string userid, UserShipAddressDTO address)
        {
            return "ok,modify address success";
        }
    }
}
