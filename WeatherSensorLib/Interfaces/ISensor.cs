using System;
using System.Collections.Generic;
using System.Text;

namespace Virtustream.WeatherSensorLib.Interfaces
{
    public interface ISensor
    {
        Guid Id { get; }
        string Name { get; }
        string City { get; }

        void ChangeName(string newName);
        void ChangeCity(string newCity);
    }
}
