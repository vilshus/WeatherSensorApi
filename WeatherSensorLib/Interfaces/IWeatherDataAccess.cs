using System;
using System.Collections.Generic;
using System.Text;

namespace Virtustream.WeatherSensorLib.Interfaces
{
    public interface IWeatherDataAccess
    {
        bool TryGetWeatherData(string city, List<DateTime> days, out List<DayWeatherData> cityWeatherData);

        void StoreWeatherData(List<DayWeatherData> dayWeatherDatas);
    }
}
