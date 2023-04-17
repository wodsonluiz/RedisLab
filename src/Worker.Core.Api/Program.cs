using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;
using System.Text.Json.Serialization;

namespace Worker.Core.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var configurations = new[]
            {
                new RedisConfiguration
                {
                    AbortOnConnectFail = true,
                    //KeyPrefix = "MyPrefix__",
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
                    //KeyPrefix = "MyPrefix__",
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
                    //KeyPrefix = "MyPrefix__",
                    Hosts = new[] { new RedisHost { Host = "localhost", Port = 6376 } },
                    AllowAdmin = true,
                    Password= "admin123",
                    ConnectTimeout = 5000,
                    Database = 2,
                    PoolSize = 2,
                    Name = "Third Instance"
                }
            };

            builder.Services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(configurations);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}