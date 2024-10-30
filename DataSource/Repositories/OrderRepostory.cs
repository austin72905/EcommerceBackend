using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace DataSource.Repositories
{
    public class OrderRepostory: IOrderRepostory
    {
        public IEnumerable<Order> GetOrdersByUserId(string userid)
        {
            throw new NotImplementedException();
        }

        public Order? GetOrderInfoByUserId(string userid, string recordCode)
        {
            throw new NotImplementedException();
        }

        public void GenerateOrder()
        {
            throw new NotImplementedException();
        }
    }
}
