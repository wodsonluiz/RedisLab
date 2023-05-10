using Microsoft.AspNetCore.Mvc;
using Redis.EasyConnectMultiServers.Abstractions;

namespace Worker.Core.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRedisProviderMultiServers _redisProviderMultiServers;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRedisProviderMultiServers redisProviderMultiServers)
        {
            _logger = logger;
            _redisProviderMultiServers = redisProviderMultiServers;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var guid = Guid.NewGuid().ToString();

            _redisProviderMultiServers.AddMultiAsync(guid, "wod 6");

            var result = _redisProviderMultiServers.GetMultiAsync<string>(guid.ToString()).GetAwaiter().GetResult();


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}