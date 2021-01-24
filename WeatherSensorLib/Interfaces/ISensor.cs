using System;

namespace Virtustream.WeatherSensorLib.Interfaces
{
    public interface ISensor
    {
        /// <summary>
        /// A unique ID of the sensor.
        /// </summary>
        Guid Id { get; }
        
        /// <summary>
        /// Human readible name of the sensor.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// City name to which the sensor belongs (e.g. Kaunas, Vilnius).
        /// </summary>
        string City { get; }

        /// <summary>
        /// Change sensor name.
        /// </summary>
        /// <param name="newName">New name of the sensor.</param>
        void ChangeName(string newName);
        
        /// <summary>
        /// Change city name.
        /// </summary>
        /// <param name="newCity">New name of the city.</param>
        void ChangeCity(string newCity);
    }
}
