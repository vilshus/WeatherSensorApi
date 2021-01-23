using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Virtustream.WeatherSensorLib.Interfaces;

namespace WeatherSensorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherSensorsController : ControllerBase
    {
        private readonly ILogger<WeatherSensorsController> logger;
        private readonly ISensorManager sensorManager;

        public WeatherSensorsController(ILogger<WeatherSensorsController> logger, ISensorManager sensorManager)
        {
            this.logger = logger;
            this.sensorManager = sensorManager;
        }

        [HttpGet]
        public ActionResult<List<ISensor>> Get()
        {
            logger.LogInformation("Root endpoint called.");
            return GetSensor(null);
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
                var message = $"Given ID {id} is in wrong format. Should be a Guid.";
                logger.LogError(message);
                return BadRequest(message);
            }

            var sensor = sensorManager.GetSensor(Guid.Parse(id));

            if (sensor == null)
            {
                var message = $"Sensor ID was not found: {id}";
                logger.LogError(message);
                return NotFound(message);
            }

            return Ok(sensor);
        }

        [Route("sensor")]
        [HttpPut]
        public ActionResult<string> CreateSensor(string name, string city)
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

        [Route("sensor")]
        [HttpPost]
        public ActionResult<string> UpdateSensor(string id, string name, string city)
        {
            logger.LogInformation($"Sensor update has been requested: ID = {id} Name = {name}, City = {city}");

            if (!Guid.TryParse(id, out Guid guid))
            {
                var message = $"Given ID {id} is in wrong format. Should be a Guid.";
                logger.LogError(message);
                return BadRequest(message);
            }

            try
            {
                if (!sensorManager.UpdateSensor(guid, name, city))
                {
                    var message = $"Sensor ID was not found: {id}";
                    logger.LogError(message);
                    return NotFound(message);
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

        [Route("sensor")]
        [HttpDelete]
        public ActionResult<string> DeleteSensor(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                var message = $"Given ID {id} is in wrong format. Should be a Guid.";
                logger.LogError(message);
                return BadRequest(message);
            }

            if (!sensorManager.DeleteSensor(guid))
            {
                return NotFound($"Sensor ID = {id} was not found.");
            }

            return Ok($"Sensor has been deleted. ID = {id}");
        }
    }
}
