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
            return SensorGet(null);
        }

        [Route("sensor")]
        [HttpGet]
        public ActionResult<List<ISensor>> SensorGet(string id)
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
    }
}
