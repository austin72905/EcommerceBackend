using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IOrderRepostory
    {
        public Task<IEnumerable<Order>> GetOrdersByUserId(int userid);

        public Task<Order?> GetOrderInfoById(int orderId);

        public Task<Order?> GetOrderInfoByUserId(int userid, string recordCode);

        public Task GenerateOrder(Order order);

        
        public Task UpdateOrderStatusAsync(string recordCode, int status);

        public Task SaveChangesAsync();
    }
}
