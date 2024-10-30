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
        public IEnumerable<Order> GetOrdersByUserId(string userid);

        public Order? GetOrderInfoByUserId(string userid, string recordCode);

        public void GenerateOrder();
    }
}
