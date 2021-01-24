using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Virtustream.WeatherSensorLib.Sensors;
using Virtustream.WeatherSensorLib.WeatherData;

namespace Virtustream.WeatherSensorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherSensorsController : ControllerBase
    {
        #region

        private const int DEFAULT_NUMBER_FORECAST_DAYS = 2;

        #endregion

        #region Private fields

        private readonly ILogger<WeatherSensorsController> logger;
        private readonly ISensorManager sensorManager;
        private readonly IWeatherDataManager weatherDataManager;

        private string WrongIdFormatMessage(string id) => $"Given ID {id} is in wrong format. Should be a Guid.";
        private string SensorIdNotFoundMessage(string id) => $"Sensor ID was not found: {id}.";

        #endregion

        public WeatherSensorsController(ILogger<WeatherSensorsController> logger, ISensorManager sensorManager, IWeatherDataManager weatherDataManager)
        {
            this.logger = logger;
            this.sensorManager = sensorManager;
            this.weatherDataManager = weatherDataManager;
        }

        #region HTTP requests

        [HttpGet]
        public ActionResult<string> RootGet()
        {
            logger.LogInformation("Root endpoint called.");
            var message = "Hello! User guide can be found: https://github.com/vilshus/WeatherSensorApi";
            return Ok(message);
        }

        [Route("sensor")]
        [HttpGet]
        public ActionResult<List<ISensor>> GetSensor(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                logger.LogInformation($"Returning information of all existing sensors.");
                return Ok(sensorManager.GetSensors().ToList());
            }

            logger.LogInformation($"Sensor information requested at ID: {id}");

            if (!Guid.TryParse(id, out Guid guid))
            {
                logger.LogError(WrongIdFormatMessage(id));
                return BadRequest(WrongIdFormatMessage(id));
            }

            var sensor = sensorManager.GetSensor(guid);

            if (sensor == null)
            {
                logger.LogError(SensorIdNotFoundMessage(id));
                return NotFound(SensorIdNotFoundMessage(id));
            }

            logger.LogInformation($"Sensor information has been returned for ID = {sensor.Id}");
            return Ok(sensor);
        }

        [Route("sensor")]
        [HttpPut]
        public ActionResult<string> PutSensor(string id, string name, string city)
        {
            if (string.IsNullOrEmpty(id))
            {
                return CreateSensor(name, city);
            }
            else
            {
                return UpdateSensor(id, name, city);
            }
        }

        [Route("sensor")]
        [HttpDelete]
        public ActionResult<string> DeleteSensor(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                logger.LogError(WrongIdFormatMessage(id));
                return BadRequest(WrongIdFormatMessage(id));
            }

            if (!sensorManager.DeleteSensor(guid))
            {
                logger.LogError(SensorIdNotFoundMessage(id));
                return NotFound(SensorIdNotFoundMessage(id));
            }

            string message = $"Sensor has been deleted. ID = {id}";
            logger.LogInformation(message);
            return Ok(message);
        }

        [Route("sensordata")]
        [HttpGet]
        public ActionResult<DayWeatherData> GetWeatherData(string id, int days)
        {
            logger.LogInformation($"Data requested from sensor ID = {id}");

            if (!Guid.TryParse(id, out Guid guid))
            {
                logger.LogError(WrongIdFormatMessage(id));
                return BadRequest(WrongIdFormatMessage(id));
            }

            var sensor = sensorManager.GetSensor(guid);

            if (sensor == null)
            {
                logger.LogError(SensorIdNotFoundMessage(id));
                return NotFound(SensorIdNotFoundMessage(id));
            }

            //If number of days is not provided then use the default value.
            days = days == 0 ? DEFAULT_NUMBER_FORECAST_DAYS : days;
            var result = weatherDataManager.GetWeatherForecastData(sensor, days);

            logger.LogInformation($"Weather data for city {sensor.City} has been retrieved.");
            return Ok(result);
        }

        #endregion

        #region Private methods

        private ActionResult<string> CreateSensor(string name, string city)
        {
            try
            {
                logger.LogInformation($"Sensor creation has been requested: Name = {name}, City = {city}");
                var sensor = sensorManager.CreateSensor(name, city);
                logger.LogInformation($"Sensor ID = {sensor.Id} has been created.");
                return Ok($"Sensor has been created. ID = {sensor.Id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        private ActionResult<string> UpdateSensor(string id, string name, string city)
        {
            logger.LogInformation($"Sensor update has been requested: ID = {id} Name = {name}, City = {city}");

            if (!Guid.TryParse(id, out Guid guid))
            {
                logger.LogError(WrongIdFormatMessage(id));
                return BadRequest(WrongIdFormatMessage(id));
            }

            try
            {
                if (!sensorManager.UpdateSensor(guid, name, city))
                {
                    logger.LogError(SensorIdNotFoundMessage(id));
                    return NotFound(SensorIdNotFoundMessage(id));
                }

                logger.LogInformation($"Sensor ID = {id} has been updated.");
                return Ok($"Sensor has been updated. ID = {id}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        #endregion
    }
}
