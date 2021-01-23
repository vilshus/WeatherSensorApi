using System;
using System.Collections.Generic;
using System.Text;

using Virtustream.WeatherSensorLib.Interfaces;

namespace Virtustream.WeatherSensorLib
{
    public class Sensor : ISensor
    {
        public Guid Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string City => throw new NotImplementedException();

        public void ChangeCity(string newCity)
        {
            throw new NotImplementedException();
        }

        public void ChangeName(string newName)
        {
            throw new NotImplementedException();
        }
    }
}
