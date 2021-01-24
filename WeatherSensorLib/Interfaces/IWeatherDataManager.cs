using System.Collections.Generic;

namespace Virtustream.WeatherSensorLib.Interfaces
{
    public interface IWeatherDataManager
    {
        List<DayWeatherData> GetWeatherData(ISensor sensor, int numberOfDays);
    }
}
