using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        public Task<Payment?> GetTenantConfig(string recordCode);

        public  Task GeneratePaymentRecord(Payment payment);

        public Task<Payment?> GetPaymentRecord(string recordCode);

        /// <summary>
        /// 獲取支付記錄（帶追蹤，用於更新訂單狀態）
        /// </summary>
        public Task<Payment?> GetPaymentRecordForUpdate(string recordCode);

        public Task SaveChangesAsync();
    }
}
