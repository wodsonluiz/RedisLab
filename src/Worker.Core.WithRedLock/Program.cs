using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using System.Net;

namespace Worker.Core.WithRedLock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var key = $"lab:teste:redis:{Guid.NewGuid()}";

                var configNew = new ConfigurationOptions
                {
                    EndPoints = 
                    {
                        new DnsEndPoint("localhost", 6378)
                    },
                    AsyncTimeout = 100,
                    Password = "admin123",
                    DefaultDatabase = 2,
                    Ssl = false,
                    AbortOnConnectFail = false,
                    AllowAdmin = false
                };

                var configNew2 = new ConfigurationOptions
                {
                    EndPoints =
                    {
                        new DnsEndPoint("localhost", 6376)
                    },
                    AsyncTimeout = 100,
                    Password = "admin123",
                    DefaultDatabase = 2,
                    Ssl = false,
                    AbortOnConnectFail = false,
                    AllowAdmin = false
                };

                var existingConnectionMultiplexer1 = ConnectionMultiplexer.Connect(configNew);
                var existingConnectionMultiplexer2 = ConnectionMultiplexer.Connect(configNew2);

                var multiplexers = new List<RedLockMultiplexer>
                {
                    existingConnectionMultiplexer2,
                    existingConnectionMultiplexer1
                };

                using (var redlockFactory = RedLockFactory.Create(multiplexers))
                {
                    var ttl = TimeSpan.FromSeconds(400);
                    redlockFactory.CreateLockAsync(key, ttl).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}