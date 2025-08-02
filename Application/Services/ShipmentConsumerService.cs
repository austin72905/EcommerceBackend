using Common.Interfaces.Infrastructure;
using Infrastructure.MQ;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ShipmentConsumerService : IHostedService
    {

        private readonly IShipmentConsumer _shipmentConsumer;

        public ShipmentConsumerService(IShipmentConsumer shipmentConsumer)
        {
            _shipmentConsumer = shipmentConsumer;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 啟動 Consumer
            Task.Run(() => _shipmentConsumer.StartListening(), cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
