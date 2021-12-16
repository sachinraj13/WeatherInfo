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
using WeatherSvc.Services.Interface;
using WeatherSvc.Models;

namespace WeatherSvc.Services
{
    public class WeatherService : IWeatherService
    {
        public WeatherService()
        {

        }
        IWeatherService _service = null;
        public async Task<List<IActionResult>> citiwiseWeather()
        {
            string cityfilepath = "cities_list.txt";

            try
            {
                List<string> allLinesText = System.IO.File.ReadAllLines(cityfilepath).ToList();

                List<IActionResult> weatherDetails = new List<IActionResult>();
                foreach (var item in allLinesText)
                {
                    string[] authorsList = item.Split("=");
                    String eachCity = authorsList[1];
                    var result = await getWeatherDetailsByCity(eachCity);
                    weatherDetails.Add(result);
                }

                writeTofile(weatherDetails);
                return weatherDetails;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> getWeatherDetailsByCity(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=aa69195559bd4f88d79f9aadeb77a8f6&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = response.Content.ReadAsStringAsync().Result;
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                    return new OkObjectResult(new WeatherInfo
                    {
                        Temp = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        City = rawWeather.Name
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    throw httpRequestException;
                }
            }
        }

        public async void writeTofile(List<IActionResult> weatherDetails)
        {
            try
            {
                if (!Directory.Exists("OutputDir/" + DateTime.Now.Date.ToString("MM/dd/yyyy")))
                {
                    Directory.CreateDirectory("OutputDir/" + DateTime.Now.Date.ToString("MM/dd/yyyy"));
                }

                foreach (IActionResult line in weatherDetails)
                {
                    WeatherInfo info = (WeatherInfo)(((OkObjectResult)line).Value);
                    using StreamWriter file = new(string.Concat("OutputDir/", DateTime.Now.Date.ToString("MM/dd/yyyy"), "/", info.City, ".txt"));
                    await file.WriteLineAsync(info.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
