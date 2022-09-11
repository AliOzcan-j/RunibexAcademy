using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Utilities.MessageBrokers.RabbitMQ
{
    public class RabbitMQPublisher : IMessageBroker
    {
        private RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish<T>(T @event)
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(@event);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);
            var property = channel.CreateBasicProperties();
            property.Persistent = true;

            channel.BasicPublish(
                exchange:_rabbitMQClientService.ExchangeName,
                routingKey: _rabbitMQClientService.RoutingKey,
                basicProperties: property,
                body: bodyByte
                );
        }
    }
}
