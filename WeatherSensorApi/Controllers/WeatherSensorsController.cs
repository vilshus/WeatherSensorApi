using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherSensorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherSensorsController : ControllerBase
    {
        private readonly ILogger<WeatherSensorsController> logger;

        public WeatherSensorsController(ILogger<WeatherSensorsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            logger.LogInformation("Root endpoint called.");
            return SensorGet();
        }

        [Route("sensor")]
        [HttpGet]
        public string SensorGet()
        {
            return "Ok";
        }
    }
}
