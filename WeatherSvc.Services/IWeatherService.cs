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

namespace WeatherSvc.Services.Interface
{
    public interface IWeatherService
    {
        Task<List<IActionResult>> citiwiseWeather();
        Task<IActionResult> getWeatherDetailsByCity(string city);
        void writeTofile(List<IActionResult> weatherDetails);

    }
}