using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Virtustream.WeatherSensorApi.Controllers;
using Virtustream.WeatherSensorLib.Interfaces;

namespace Virtustream.WeatherSensorApi.Tests
{
    [TestClass]
    public sealed class WeatherSensorControllerTest
    {

        /// <summary>
        /// Test purpose:
        ///     Test if PUT request returns OK() when the given ID exists.
        /// Expected result:
        ///     Put request returns Ok() ActionResult.
        /// </summary>
        [TestMethod]
        public void PutSensor_SensorIdExists_ReturnsOk()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<WeatherSensorsController>>();
            var weatherDataManagerMock = new Mock<IWeatherDataManager>(MockBehavior.Strict);
            var sensorManagerMock = new Mock<ISensorManager>(MockBehavior.Strict);

            
            sensorManagerMock.Setup(x => x.UpdateSensor(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var apiController = new WeatherSensorsController(loggerMock.Object, sensorManagerMock.Object, weatherDataManagerMock.Object);

            //Act
            var response = apiController.PutSensor(Guid.NewGuid().ToString(), "Kaunas 1", "Kaunas");

            //Assert
            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));
        }

        /// <summary>
        /// Test purpose:
        ///     Test if PUT request returns NotFound() when the given ID does not exist.
        /// Expected result:
        ///     Put request returns NotFound() ActionResult.
        /// </summary>
        [TestMethod]
        public void PutSensor_SensorIdDoesntExist_ReturnsNotFound()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<WeatherSensorsController>>();
            var weatherDataManagerMock = new Mock<IWeatherDataManager>(MockBehavior.Strict);
            var sensorManagerMock = new Mock<ISensorManager>(MockBehavior.Strict);

            sensorManagerMock.Setup(x => x.UpdateSensor(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var apiController = new WeatherSensorsController(loggerMock.Object, sensorManagerMock.Object, weatherDataManagerMock.Object);

            //Act
            var response = apiController.PutSensor(Guid.NewGuid().ToString(), "Kaunas 1", "Kaunas");

            //Assert
            Assert.IsInstanceOfType(response.Result, typeof(NotFoundObjectResult));
        }
    }
}
