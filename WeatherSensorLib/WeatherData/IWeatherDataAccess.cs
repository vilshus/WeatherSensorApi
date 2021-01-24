using System;
using System.Collections.Generic;

namespace Virtustream.WeatherSensorLib.WeatherData
{
    public interface IWeatherDataAccess
    {
        /// <summary>
        /// Gets weather data for the requested city and days.
        /// </summary>
        /// <param name="city">Name of the city.</param>
        /// <param name="days">Days for which the forecast data will be returned. </param>
        /// <param name="cityWeatherData">A result of all day weather datas which where found.</param>
        /// <returns>True if weather datas were found for ALL days. False otherwise.</returns>
        bool TryGetWeatherData(string city, List<DateTime> days, out List<DayWeatherData> cityWeatherData);

        /// <summary>
        /// Persists the weather datas in the storage (e.g. memory, DataBase...).
        /// </summary>
        /// <param name="dayWeatherDatas">Day weather datas to be stored.</param>
        void StoreWeatherData(List<DayWeatherData> dayWeatherDatas);
    }
}
