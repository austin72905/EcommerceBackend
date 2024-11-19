

using Application.DTOs;
using StackExchange.Redis;

namespace Application.Interfaces
{
    public interface ICartService
    {      
        public Task<ServiceResult<List<ProductWithCountDTO>>> MergeCartContent(int userid,List<CartItemDTO> frontEndCartItems,bool isCover);
    }
}
