using System.Collections.Generic;
using Virtustream.WeatherSensorLib.Sensors;

namespace Virtustream.WeatherSensorLib.WeatherData
{
    public interface IWeatherDataManager
    {
        /// <summary>
        /// Get weather forecast for the next <see cref="numberOfDays"/> days.
        /// </summary>
        /// <param name="sensor">Sensor for which the forecast data is retrieved.</param>
        /// <param name="numberOfDays">Number of weather forecast days.</param>
        /// <returns>Day forecast datas for the city fo the sensor.</returns>
        List<DayWeatherData> GetWeatherForecastData(ISensor sensor, int numberOfDays);
    }
}
