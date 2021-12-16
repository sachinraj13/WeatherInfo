using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Text;
//using WeatherSvc.Entity;
using WeatherSvc.Models;
using WeatherSvc.Services;
using WeatherSvc.Services.Interface;

namespace WeatherSvc.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet]
        [Route("citiwiseWeather")]
        public async Task<IActionResult> citiwiseWeather()
        {

            IWeatherService _service = null;
            //ILogger<WeatherService> _logger;

            _service = new WeatherService();
            try
            {
                Task<List<IActionResult>> weatherDetails = _service.citiwiseWeather();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting weather from OpenWeather: {ex.Message}");
            }

        }

    }

}
