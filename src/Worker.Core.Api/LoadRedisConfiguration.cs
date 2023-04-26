using Microsoft.Extensions.Configuration;
using StackExchange.Redis.Extensions.Core.Configuration;

namespace Worker.Core.Api
{
    public static class LoadRedisConfiguration
    {
        public static IEnumerable<RedisConfiguration> GetRedisConfigurations()
        {
            yield return new RedisConfiguration
            {
                AbortOnConnectFail = true,
                Hosts = new[] { new RedisHost { Host = "localhost", Port = 6376 } },
                AllowAdmin = false,
                Password = "admin123",
                SyncTimeout = 5000,
                Database = 2,
                PoolSize = 2,
                Name = "First Instance",
                Ssl = false,
            };

            yield return new RedisConfiguration
            {
                AbortOnConnectFail = true,
                Hosts = new[] { new RedisHost { Host = "localhost", Port = 6378 } },
                AllowAdmin = false,
                Password = "admin123",
                SyncTimeout = 5000,
                Database = 2,
                PoolSize = 2,
                Name = "Secndary Instance",
                Ssl = false,
                IsDefault = true,
            };
        }
    }
}
