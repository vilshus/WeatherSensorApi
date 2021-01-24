using System;
using System.Collections.Generic;

namespace Virtustream.WeatherSensorLib.Interfaces
{
    /// <summary>
    /// Interface for the management persistance of sensors.
    /// </summary>
    public interface ISensorManager
    {
        /// <summary>
        /// Creates a sensor and stores it in the internal structure.
        /// </summary>
        /// <param name="name">Name of the sensor.</param>
        /// <param name="city">City where the sensor is.</param>
        /// <returns>Created sensor.</returns>
        ISensor CreateSensor(string name, string city);

        /// <summary>
        /// Deletes sensor by the given ID.
        /// </summary>
        /// <param name="id">ID of the sensor to be deleted.</param>
        /// <returns>True if sensor was found and deleted.</returns>
        bool DeleteSensor(Guid id);

        /// <summary>
        /// Updates sensor information.
        /// </summary>
        /// <param name="id">ID of the sensor to be updated.</param>
        /// <param name="newName">New name of the sensor.</param>
        /// <param name="newCity">New city of the sensor.</param>
        /// <returns>True if a given sensor was found and updated.</returns>
        bool UpdateSensor(Guid id, string newName, string newCity);

        /// <summary>
        /// Get sensor by it's ID.
        /// </summary>
        /// <param name="id">Id of the sensor.</param>
        /// <returns>Found sensor or <see cref="null"/> if it was not found.</returns>
        ISensor GetSensor(Guid id);

        /// <summary>
        /// Get all existing sensors.
        /// </summary>
        /// <returns>All existing sensors.</returns>
        IEnumerable<ISensor> GetSensors();
    }
}
