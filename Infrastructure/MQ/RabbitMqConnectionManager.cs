using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MQ
{
    /// <summary>
    /// RabbitMQ 連接管理器，用於重用連接和 channel，避免每次發送消息都創建新連接
    /// </summary>
    public class RabbitMqConnectionManager : IDisposable
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqConnectionManager> _logger;
        private IConnection? _connection;
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);
        private bool _disposed = false;

        public RabbitMqConnectionManager(IConfiguration configuration, ILogger<RabbitMqConnectionManager> logger)
        {
            _logger = logger;
            var rabbitMqUri = configuration["AppSettings:RabbitMqUri"] ?? "amqp://guest:guest@localhost:5672/";
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMqUri),
                // 設置自動重連
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            };
        }

        /// <summary>
        /// 獲取或創建連接（線程安全）
        /// </summary>
        public async Task<IConnection> GetConnectionAsync()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(RabbitMqConnectionManager));
            }

            // 如果連接已存在且可用，直接返回
            if (_connection != null && _connection.IsOpen)
            {
                return _connection;
            }

            await _connectionLock.WaitAsync();
            try
            {
                // 雙重檢查，避免多線程同時創建連接
                if (_connection != null && _connection.IsOpen)
                {
                    return _connection;
                }

                // 關閉舊連接（如果存在但已關閉）
                if (_connection != null)
                {
                    try
                    {
                        // 在 7.x 版本中，IConnection 實現了 IAsyncDisposable
                        if (_connection is IAsyncDisposable asyncDisposable)
                        {
                            await asyncDisposable.DisposeAsync();
                        }
                        else
                        {
                            _connection.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "關閉舊 RabbitMQ 連接時發生錯誤");
                    }
                }

                // 創建新連接
                _connection = await _connectionFactory.CreateConnectionAsync();
                _logger.LogInformation("已創建新的 RabbitMQ 連接");

                return _connection;
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        /// <summary>
        /// 創建一個新的 channel（每次調用都創建新的 channel，但重用連接）
        /// </summary>
        public async Task<IChannel> CreateChannelAsync()
        {
            var connection = await GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();
            return channel;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _connectionLock.Wait();

            try
            {
                if (_connection != null)
                {
                    try
                    {
                        // 在 Dispose 方法中，直接調用 Dispose 即可
                        _connection.Dispose();
                        _logger.LogInformation("已關閉 RabbitMQ 連接");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "關閉 RabbitMQ 連接時發生錯誤");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "關閉 RabbitMQ 連接時發生錯誤");
            }
            finally
            {
                _connectionLock.Release();
                _connectionLock.Dispose();
            }
        }
    }
}
