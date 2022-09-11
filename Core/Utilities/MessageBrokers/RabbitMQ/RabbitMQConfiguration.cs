using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.MessageBrokers.RabbitMQ
{
    public class RabbitMQConfiguration
    {
        public string ExchangeName;
        public string ExchangeType;
        public bool Durability;
        public bool AutoDelete;
        public string QueueName;
        public bool Exclusivity;
        public IDictionary<string, object>? Arguments;
        public string RoutingKey;
        public string Url;
        private IConfigurationRoot _configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

        public RabbitMQConfiguration()
        {
            var connection = _configuration.GetSection("RabbitMQ");
            ExchangeName = connection.GetSection("ExchangeName").Value;
            ExchangeType = connection.GetSection("ExchangeType").Value;
            Durability = bool.Parse(connection.GetSection("Durability").Value);
            AutoDelete = bool.Parse(connection.GetSection("AutoDelete").Value);
            QueueName = connection.GetSection("QueueName").Value;
            Exclusivity = bool.Parse(connection.GetSection("Exclusivity").Value);
            RoutingKey = connection.GetSection("RoutingKey").Value;

            Url = connection.GetSection("Url").Value;
        }

        public RabbitMQConfiguration(IDictionary<string, object>? arguments)
        {
            Arguments = arguments;
        }

    }
}
