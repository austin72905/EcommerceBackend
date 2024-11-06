using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IOrderDomainService
    {
        public int CalculateOrderTotal(List<OrderProduct> orderProducts, int shippingPrice);
    }
}
