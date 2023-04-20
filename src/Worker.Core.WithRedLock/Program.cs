using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using System.Net;

namespace Worker.Core.WithRedLock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var redlockEndPoints = new List<RedLockEndPoint>
            {
                new RedLockEndPoint
                {
                    EndPoints =
                    {
                        new DnsEndPoint("localhost", 6376)
                    },
                    ConnectionTimeout = 5000,
                    Password = "admin123",
                    RedisDatabase = 2,
                    
                },
                new RedLockEndPoint
                {
                    EndPoint = new DnsEndPoint("localhost", 6377),
                    ConnectionTimeout = 5000,
                    Password = "admin123",
                    RedisDatabase = 2,
                }
            };

            using (var redlockFactory = RedLockFactory.Create(redlockEndPoints))
            {
                var ttl = TimeSpan.FromSeconds(400);
                var redLock = redlockFactory.CreateLockAsync("lab:teste:redis", ttl).GetAwaiter().GetResult();
            }
        }
    }
}