

using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICartService
    {
        public ServiceResult<ProductWithCountDTO> GetCartContent(int userid);
    }
}
