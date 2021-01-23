using System;
using System.Collections.Generic;
using System.Text;

namespace Virtustream.WeatherSensorLib.Interfaces
{
    public interface IWeatherDataManager
    {
        string GetWeatherData();
        string GetWeatherData(ISensor sensor, int numberOfDays);
    }
}
