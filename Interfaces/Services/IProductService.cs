using EcommerceBackend.Models;

namespace EcommerceBackend.Interfaces.Services
{
    public interface IProductService
    {
        public ProductResponse GetProducts(string userid,string kind,string tag);
        public List<ProductInfomation> GetProductsByKind(string kind);

        public List<ProductInfomation> GetProductsByTag(string tag);
    }
}
