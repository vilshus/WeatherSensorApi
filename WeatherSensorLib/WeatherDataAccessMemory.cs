using System;
using System.Collections.Generic;
using System.Text;
using Virtustream.WeatherSensorLib.Interfaces;
using System.Linq;

namespace Virtustream.WeatherSensorLib
{
    /// <summary>
    /// Provides weather data storage and access in memory.
    /// </summary>
    public class WeatherDataAccessMemory : IWeatherDataAccess
    {
        private Dictionary<KeyValuePair<string, DateTime>, DayWeatherData> weatherDataRepo = new Dictionary<KeyValuePair<string, DateTime>, DayWeatherData>();

        public bool TryGetWeatherData(string city, List<DateTime> days, out List<DayWeatherData> cityWeatherData)
        {
            cityWeatherData = new List<DayWeatherData>();
            cityWeatherData = weatherDataRepo.Where(x => x.Key.Key == city && days.Contains(x.Key.Value)).Select(x => x.Value).ToList();
            
            if (cityWeatherData.Count != days.Count)
            {
                return false;
            }

            return true;
        }

        public void StoreWeatherData(List<DayWeatherData> dayWeatherDatas)
        {
            foreach (var dayWeatherData in dayWeatherDatas)
            {
                var weatherDataRepoKey = new KeyValuePair<string, DateTime>(dayWeatherData.City, dayWeatherData.Date);
                weatherDataRepo.Remove(weatherDataRepoKey);
                weatherDataRepo.Add(weatherDataRepoKey, dayWeatherData);
            }
        }
    }
}
