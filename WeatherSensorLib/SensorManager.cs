using System;
using System.Collections.Generic;
using System.Linq;
using Virtustream.WeatherSensorLib.Interfaces;

namespace Virtustream.WeatherSensorLib
{
    public class SensorManager : ISensorManager
    {
        #region Constants

        //Would be a good idea to externalise this into json file. At the moment this is fine though.
        private readonly Guid INITIAL_SENSOR_ID = Guid.Parse("00000000-0000-0000-0000-000000000000");
        private const string INITIAL_SENSOR_NAME = "Kaunas Sensor 1";
        private const string INITIAL_SENSOR_CITY = "Kaunas";

        #endregion

        private readonly Dictionary<Guid, ISensor> sensors = new Dictionary<Guid, ISensor>();

        public SensorManager()
        {
            CreateInitialSensor();
        }

        public ISensor CreateSensor(string name, string city)
        {
            var sensor = new Sensor(Guid.NewGuid(), name, city);
            sensors.Add(sensor.Id, sensor);

            return sensor;
        }

        public bool DeleteSensor(Guid id)
        {
            return sensors.Remove(id);
        }

        public ISensor GetSensor(Guid id)
        {
            sensors.TryGetValue(id, out ISensor sensor);

            return sensor;
        }

        public IEnumerable<ISensor> GetSensors()
        {
            return sensors.Select(x => x.Value);
        }

        public bool UpdateSensor(Guid id, string newName, string newCity)
        {
            if (!sensors.TryGetValue(id, out ISensor sensor))
            {
                return false;
            }

            sensor.ChangeName(newName);
            sensor.ChangeCity(newCity);

            return true;
        }

        private void CreateInitialSensor()
        {
            var sensor = new Sensor(INITIAL_SENSOR_ID, INITIAL_SENSOR_NAME, INITIAL_SENSOR_CITY);
            sensors.Add(sensor.Id, sensor);
        }
    }
}
