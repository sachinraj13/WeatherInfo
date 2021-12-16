using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherSvc.Entity
{
    public class WeatherDataEntity
    {

    }

    public class Description
    {
        public string Main { get; set; }
        public string Condition { get; set; }
    }

    public class Main
    {
        public string Temp { get; set; }
    }

    public class WeatherResponse
    {
        public string Name { get; set; }
        //public IEnumerable Weather { get; set; }
        public Main Main { get; set; }
    }
    public class OpenWeatherResponse
    {
        public string Name { get; set; }

        public IEnumerable<WeatherDescription> Weather { get; set; }

        public Main Main { get; set; }
    }

    public class WeatherInfo
    {
        public string Temp { get; set; }
        public string Summary { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return City + "-> " + Temp + ", " + Summary;
        }
    }

    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

}