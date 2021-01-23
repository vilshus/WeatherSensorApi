using System;
using System.Collections.Generic;
using System.Text;

using Virtustream.WeatherSensorLib.Interfaces;

namespace Virtustream.WeatherSensorLib
{
    public class Sensor : ISensor
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string City { get; private set; }

        public Sensor(Guid id, string name, string city)
        {
            if (id == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(city))
            {
                throw new InvalidOperationException($"Sensor data cannot be null or empty. ID: {id}; Name: {name}; City: {city}");
            }

            Id = id;
            Name = name;
            City = city;
        }

        public void ChangeCity(string newCity)
        {
            if (string.IsNullOrEmpty(newCity))
            {
                throw new InvalidOperationException($"Sensor city cannot be null or empty. ID: {Id}; NewCity: {newCity}");
            }

            City = newCity;
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new InvalidOperationException($"Sensor name cannot be null or empty. ID: {Id}; NewCity: {newName}");
            }

            Name = newName;
        }
    }
}
