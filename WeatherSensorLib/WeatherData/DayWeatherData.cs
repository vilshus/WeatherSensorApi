using System;
using Virtustream.WeatherSensorLib.WeatherData.ExternalApi;

namespace Virtustream.WeatherSensorLib.WeatherData
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
