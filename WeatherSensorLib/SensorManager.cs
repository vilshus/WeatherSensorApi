using System;
using System.Collections.Generic;
using System.Text;
using Virtustream.WeatherSensorLib.Interfaces;

namespace Virtustream.WeatherSensorLib
{
    public class SensorManager : ISensorManager
    {
        public ISensor CreateSensor(string name, string city)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSensor(Guid id)
        {
            throw new NotImplementedException();
        }

        public ISensor GetSensor(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISensor> GetSensors()
        {
            throw new NotImplementedException();
        }

        public bool UpdateSensor(Guid id, string newName, string newCity)
        {
            throw new NotImplementedException();
        }
    }
}
