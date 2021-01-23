using System;
using System.Collections.Generic;
using System.Text;
using Virtustream.WeatherSensorLib.ExternalApi;

namespace Virtustream.WeatherSensorLib
{
    public class DayWeatherData
    {
        public string City { get; }
        public DateTime Date { get; }
        public Forecastday WeatherData { get; }

        public DayWeatherData(string city, DateTime date, Forecastday weatherData)
        {
            City = city;
            Date = date;
            WeatherData = weatherData;
        }
    }
}
