using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestServiceNet5WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPut("api/v1/{ForecastOldName}/{ForecastNewName}")]
        public IActionResult UpdateWeatherForecast(string ForecastOldName, string ForecastNewName)
        {
            var positions = Summaries.Select((v, i) => new { Index = i, Value = v })
                               .Where(p => p.Value == ForecastOldName)
                               .Select(p => p.Index);

            foreach (int position in positions)
            {
                Summaries[position] = ForecastNewName;
            }

            return Ok(Summaries);

        }

        [HttpDelete("api/v1/{ForecastPosition}")]
        public IActionResult DeleteWeatherForecast(int ForecastPosition)
        {
            Summaries[ForecastPosition] = "";
            return Ok(Summaries);
        }
    }
}
