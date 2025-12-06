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

        /// <summary>
        /// 獲取支付狀態（輕量級查詢，只返回 PaymentStatus，不載入完整實體）
        /// 用於快速檢查是否已處理，避免不必要的鎖等待
        /// </summary>
        public Task<byte?> GetPaymentStatusAsync(string recordCode);

        /// <summary>
        /// 獲取單一租戶配置（整個應用使用同一個配置）
        /// </summary>
        public Task<TenantConfig?> GetDefaultTenantConfigAsync();

        /// <summary>
        /// 根據訂單號獲取支付記錄（不包含 TenantConfig，因為已從緩存獲取）
        /// </summary>
        public Task<Payment?> GetPaymentByRecordCode(string recordCode);

        public Task SaveChangesAsync();

        /// <summary>
        /// 嘗試將支付狀態從未付款更新為已付款（單一 SQL，避免重複處理）
        /// </summary>
        /// <param name="recordCode">訂單編號</param>
        /// <returns>更新結果，Updated=false 代表已處理或不存在</returns>
        public Task<PaymentUpdateResult> TryMarkPaymentAsPaidAsync(string recordCode);
    }

    public record PaymentUpdateResult(bool Updated, int PaymentId, int OrderId, int PayWay);
}
