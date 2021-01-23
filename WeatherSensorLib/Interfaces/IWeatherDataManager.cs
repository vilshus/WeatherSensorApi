using System;
using System.Collections.Generic;
using System.Text;

namespace Virtustream.WeatherSensorLib.Interfaces
{
    public interface IWeatherDataManager
    {
        List<DayWeatherData> GetWeatherData(ISensor sensor, int numberOfDays);
    }
}
