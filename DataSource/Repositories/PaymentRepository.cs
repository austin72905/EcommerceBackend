using System.Data;
using DataSource.DBContext;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataSource.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(EcommerceDBContext context) : base(context)
        {
        }

        public async Task<Payment?> GetTenantConfig(string recordCode)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Order)
                .Include(p => p.TenantConfig)
                .Where(p => p.Order.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        public async Task GeneratePaymentRecord(Payment payment)
        {
            await AddPaymentWithoutSave(payment);
            await SaveChangesAsync();
        }

        /// <summary>
        /// 添加付款記錄到追蹤器但不立即保存（用於交易中批量保存）
        /// </summary>
        public async Task AddPaymentWithoutSave(Payment payment)
        {
            await _dbSet.AddAsync(payment);
            // 不調用 SaveChangesAsync，等待交易統一保存
        }

        /// <summary>
        /// 使用原生 SQL 批量插入付款記錄（高效能，避免 EF Core 追蹤開銷）
        /// 直接執行 SQL，繞過 EF Core 的 ChangeTracker，提升性能
        /// </summary>
        public async Task BulkInsertPaymentAsync(int orderId, decimal paymentAmount, int tenantConfigId)
        {
            var now = DateTime.UtcNow;
            const string sql = @"
                INSERT INTO ""Payments"" (""OrderId"", ""TenantConfigId"", ""PaymentAmount"", ""PaymentStatus"", ""TransactionId"", ""CreatedAt"", ""UpdatedAt"")
                VALUES (@orderId, @tenantConfigId, @paymentAmount, 1, '', @now, @now)";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new NpgsqlParameter("@orderId", orderId),
                new NpgsqlParameter("@tenantConfigId", tenantConfigId),
                new NpgsqlParameter("@paymentAmount", paymentAmount),
                new NpgsqlParameter("@now", now));
        }

        public async Task<Payment?> GetPaymentRecord(string recordCode)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Order)
                    .ThenInclude(p => p.Shipments)
                .Where(p => p.Order.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 獲取支付記錄（帶追蹤，用於更新訂單狀態）
        /// </summary>
        public async Task<Payment?> GetPaymentRecordForUpdate(string recordCode)
        {
            var sql = @"
                SELECT p.*
                FROM ""Payments"" p
                INNER JOIN ""Orders"" o ON p.""OrderId"" = o.""Id""
                WHERE o.""RecordCode"" = {0}
                FOR UPDATE OF p, o";

            var payment = await _dbSet
                .FromSqlRaw(sql, recordCode)
                .Include(p => p.Order)
                .FirstOrDefaultAsync();

            return payment;
        }

        /// <summary>
        /// 獲取支付狀態（輕量級查詢）
        /// </summary>
        public async Task<byte?> GetPaymentStatusAsync(string recordCode)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p => p.Order.RecordCode == recordCode)
                .Select(p => (byte?)p.PaymentStatus)
                .FirstOrDefaultAsync();
        }

        public async Task<TenantConfig?> GetDefaultTenantConfigAsync()
        {
            return await _context.TenantConfigs
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Payment?> GetPaymentByRecordCode(string recordCode)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Order)
                .Where(p => p.Order.RecordCode == recordCode)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 嘗試將支付狀態從未付款更新為已付款（單一 SQL，避免重複處理）
        /// </summary>
        public async Task<PaymentUpdateResult> TryMarkPaymentAsPaidAsync(string recordCode)
        {
            // PaymentStatus: 2 表示已付款（對應原 OrderStepStatus.PaymentReceived）
            var paidStatus = (byte)2;
            var waitingForShipment = (int)Domain.Enums.OrderStatus.WaitingForShipment;
            var now = DateTime.UtcNow;

            const string sql = @"
WITH updated AS (
    UPDATE ""Payments"" p
    SET ""PaymentStatus"" = @paidStatus,
        ""UpdatedAt"" = @now
    FROM ""Orders"" o
    WHERE p.""OrderId"" = o.""Id""
      AND o.""RecordCode"" = @recordCode
      AND p.""PaymentStatus"" <> @paidStatus
    RETURNING TRUE AS ""Updated"", p.""Id"" AS ""PaymentId"", p.""OrderId"" AS ""OrderId"", o.""PayWay"" AS ""PayWay""
),
order_updated AS (
    UPDATE ""Orders"" o
    SET ""Status"" = @waitingForShipment,
        ""UpdatedAt"" = @now,
        ""PaidAt"" = @now
    WHERE o.""RecordCode"" = @recordCode
      AND o.""Status"" <> @waitingForShipment
    RETURNING 1
)
SELECT ""Updated"", ""PaymentId"", ""OrderId"", ""PayWay"" FROM updated LIMIT 1";

            await using var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            await using var command = connection.CreateCommand();
            command.CommandText = sql;

            var paidParam = command.CreateParameter();
            paidParam.ParameterName = "@paidStatus";
            paidParam.Value = paidStatus;
            command.Parameters.Add(paidParam);

            var waitingParam = command.CreateParameter();
            waitingParam.ParameterName = "@waitingForShipment";
            waitingParam.Value = waitingForShipment;
            command.Parameters.Add(waitingParam);

            var nowParam = command.CreateParameter();
            nowParam.ParameterName = "@now";
            nowParam.Value = now;
            command.Parameters.Add(nowParam);

            var codeParam = command.CreateParameter();
            codeParam.ParameterName = "@recordCode";
            codeParam.Value = recordCode;
            command.Parameters.Add(codeParam);

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var updated = reader.GetBoolean(0);
                var paymentId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                var orderId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                var payWay = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

                return new PaymentUpdateResult(updated, paymentId, orderId, payWay);
            }

            return new PaymentUpdateResult(false, 0, 0, 0);
        }
    }
}
