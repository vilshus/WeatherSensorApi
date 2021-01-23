using System;
using System.Collections.Generic;
using System.Text;

using Virtustream.WeatherSensorLib.Interfaces;

namespace Virtustream.WeatherSensorLib
{
    public class WeatherDataManager : IWeatherDataManager
    {
        public string GetWeatherData()
        {
            throw new NotImplementedException();
        }

        public string GetWeatherData(ISensor sensor, int numberOfDays)
        {
            throw new NotImplementedException();
        }
    }
}
