using StackExchange.Redis;
using System.Net;
using Worker.Core.Console.Extensions;

namespace Worker.Core.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new ConfigurationOptions()
            {
                EndPoints = {
                    { "localhost", 6379 }, //NOVO 
                    { "localhost", 6378 },  //NOVO 2
                    { "localhost", 6377 },  //NOVO 2
                },
                //User = "default",  // use your Redis user. More info https://redis.io/docs/management/security/acl/
                Password = "admin123", // use your Redis password
                //SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                DefaultDatabase = 2,
                AllowAdmin = true,
                ConnectTimeout = 500000
            };

            var cluster = ConnectionMultiplexer.Connect(options);
            IDatabase dbFromCluster = cluster.GetDatabase();

            dbFromCluster.StringSet(Guid.NewGuid().ToString(), "bar");
            dbFromCluster.StringSetAllServers(Guid.NewGuid().ToString(), "bar");
        }
    }
}