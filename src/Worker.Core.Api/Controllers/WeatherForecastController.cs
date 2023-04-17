using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Worker.Core.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRedisClientFactory _clientFactory;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRedisClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var guid = Guid.NewGuid().ToString();
            var db = _clientFactory.GetRedisDatabase("First Instance");
            var db2 = _clientFactory.GetRedisDatabase("Secndary Instance");
            var db3 = _clientFactory.GetRedisDatabase("Third Instance");
            db.SetAddAsync(guid, "bar").GetAwaiter().GetResult();
            db2.SetAddAsync(guid, "bar").GetAwaiter().GetResult();
            db3.SetAddAsync(guid, "bar").GetAwaiter().GetResult();

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