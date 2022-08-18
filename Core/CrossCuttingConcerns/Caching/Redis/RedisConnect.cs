using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public abstract class RedisConnect
    {
        private readonly ConfigurationOptions configuration;
        private Lazy<IConnectionMultiplexer> _Connection;

        public RedisConnect()
        {
            IConfigurationRoot cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var redisConfiguration = cfg.GetSection("Redis");

            configuration = new ConfigurationOptions()
            {
                EndPoints = { { redisConfiguration.GetSection("Host").Value, int.Parse(redisConfiguration.GetSection("Port").Value) } },
                AllowAdmin = bool.Parse(redisConfiguration.GetSection("AllowAdmin").Value),
                Password = redisConfiguration.GetSection("Password").Value,
                User=redisConfiguration.GetSection("User").Value,
                AbortOnConnectFail = bool.Parse(redisConfiguration.GetSection("AbortOnConnectFail").Value),
                ClientName = redisConfiguration.GetSection("ClientName").Value,
                Ssl = bool.Parse(redisConfiguration.GetSection("Ssl").Value),
                ConnectTimeout = int.Parse(redisConfiguration.GetSection("ConnectTimeout").Value),
                ConnectRetry = int.Parse(redisConfiguration.GetSection("ConnectRetry").Value),
                DefaultDatabase = int.Parse(redisConfiguration.GetSection("Database").Value)
            };

            _Connection = new Lazy<IConnectionMultiplexer>(() =>
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);
                return redis;
            });

        }

        public IConnectionMultiplexer Connection { get { return _Connection.Value; } }
        public IDatabase Database => Connection.GetDatabase();
    }
}
