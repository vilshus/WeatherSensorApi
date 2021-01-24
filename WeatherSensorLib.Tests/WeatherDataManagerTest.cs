using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using Virtustream.WeatherSensorLib.WeatherData;
using Virtustream.WeatherSensorLib.WeatherData.ExternalApi;
using Virtustream.WeatherSensorLib.Sensors;

namespace Virtustream.WeatherSensorLib.Tests
{
    [TestClass]
    public sealed class WeatherDataManagerTest
    {
        /// <summary>
        /// Test purpose:
        ///     Test if weather data is returned when it is found in local repo.
        /// Expected result:
        ///     Get weather data returns locally found data.
        /// </summary>
        [TestMethod]
        public void GetWeatherData_DataIsFound_ReturnsData()
        {
            //Arrange
            var dayWeatherDatas = new List<DayWeatherData> { new DayWeatherData("Kaunas", DateTime.Now, new Forecastday()) };
            var days = new List<DateTime> { DateTime.Today.AddDays(1) };
            var city = "Kaunas";

            var sensorMock = new Mock<ISensor>(MockBehavior.Strict);
            sensorMock.Setup(x => x.City).Returns(city);

            var weatherDataRepoMock = new Mock<IWeatherDataAccess>(MockBehavior.Strict);
            weatherDataRepoMock.Setup(x => x.TryGetWeatherData(city, days, out dayWeatherDatas)).Returns(true);

            var weatherDataManager = new WeatherDataManager(weatherDataRepoMock.Object);

            //Act
            var result = weatherDataManager.GetWeatherForecastData(sensorMock.Object, 1);

            //Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(dayWeatherDatas[0], result[0]);
        }
    }
}
