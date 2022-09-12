using Core.Utilities.MessageBrokers.Events;
using Core.Utilities.MessageBrokers.Messages;
using Core.Utilities.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.BackgroundServices
{
    public class EmailResponseBackgroundService : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public EmailResponseBackgroundService(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        private IModel _channel;

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 5, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: _rabbitMQClientService.QueueName, autoAck: false, consumer: subscriber);
            subscriber.Received += Subscriber_Received;

            return Task.CompletedTask;
        }

        private Task Subscriber_Received(object sender, BasicDeliverEventArgs @event)
        {
            var userRegisteredEvent = JsonSerializer.Deserialize<UserRegisteredEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            var emailResponse = new CreateEmailResponseMessage() { UserEmail = userRegisteredEvent.UserEmail, EmailRespone = $"Hoşgeldiniz {userRegisteredEvent.UserEmail}" };
            SendEmail(emailResponse);

            _channel.BasicAck(@event.DeliveryTag, true);

            return Task.CompletedTask;
        }

        private Task SendEmail(CreateEmailResponseMessage emailResponse)
        {
            Console.WriteLine(emailResponse.EmailRespone);
            return Task.CompletedTask;
        }
    }
}
