using StackExchange.Redis.Extensions.Core.Configuration;

namespace Worker.Core.Api
{
    public static class LoadRedisConfiguration
    {
        public static RedisConfiguration[] Get()
        {
            var configurations = new[]
            {
                new RedisConfiguration
                {
                    AbortOnConnectFail = true,
                    Hosts = new[] { new RedisHost { Host = "localhost", Port = 6379 } },
                    AllowAdmin = true,
                    ConnectTimeout = 5000,
                    Password= "admin123",
                    Database = 2,
                    PoolSize = 5,
                    IsDefault = true,
                    Name = "First Instance"
                },
                new RedisConfiguration
                {
                    AbortOnConnectFail = true,
                    Hosts = new[] { new RedisHost { Host = "localhost", Port = 6378 } },
                    AllowAdmin = true,
                    Password= "admin123",
                    ConnectTimeout = 5000,
                    Database = 2,
                    PoolSize = 2,
                    Name = "Secndary Instance"
                },
                new RedisConfiguration
                {
                    AbortOnConnectFail = true,
                    Hosts = new[] { new RedisHost { Host = "localhost", Port = 6376 } },
                    AllowAdmin = true,
                    Password= "admin123",
                    ConnectTimeout = 5000,
                    Database = 2,
                    PoolSize = 2,
                    Name = "Third Instance"
                }
            };

            return configurations;
        }
    }
}
