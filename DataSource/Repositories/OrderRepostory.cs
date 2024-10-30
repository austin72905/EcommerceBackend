using DataSource.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Repositories
{
    public class OrderRepostory
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
