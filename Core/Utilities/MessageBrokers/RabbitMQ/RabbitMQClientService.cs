using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.MessageBrokers.RabbitMQ
{
    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly RabbitMQConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        public string ExchangeName => _configuration.ExchangeName;
        public string RoutingKey => _configuration.RoutingKey;

        public RabbitMQClientService(ConnectionFactory connectionFactory, RabbitMQConfiguration configuration)
        {
            _connectionFactory = connectionFactory;
            _configuration = configuration;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen:true})
            {
                return _channel;
            }

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(
                exchange: _configuration.ExchangeName,
                type: _configuration.ExchangeType,
                durable: _configuration.Durability,
                autoDelete: _configuration.AutoDelete
                );
            _channel.QueueDeclare(
                queue: _configuration.QueueName,
                durable: _configuration.Durability,
                exclusive: _configuration.Exclusivity,
                autoDelete: _configuration.AutoDelete,
                arguments: _configuration.Arguments
                );
            _channel.QueueBind(
                exchange: _configuration.ExchangeName,
                queue: _configuration.QueueName,
                routingKey: _configuration.RoutingKey
                );

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
