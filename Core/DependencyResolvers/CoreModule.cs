using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.MessageBrokers;
using Core.Utilities.MessageBrokers.RabbitMQ;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Core.BackgroundServices;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        private IConfigurationRoot _configuration;

        public CoreModule(IConfigurationBuilder configuration)
        {
            this._configuration = configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, RedisCacheManager>();
            serviceCollection.AddSingleton<RabbitMQConfiguration>();
            serviceCollection.AddSingleton(sp => new ConnectionFactory()
            {
                Uri = new Uri(_configuration.GetSection("RabbitMQ:Url").Value),
                DispatchConsumersAsync = true
            });
            serviceCollection.AddSingleton<RabbitMQClientService>();
            serviceCollection.AddSingleton<IMessageBroker, RabbitMQPublisher>();
            serviceCollection.AddHostedService<EmailResponseBackgroundService>();
        }
    }
}
