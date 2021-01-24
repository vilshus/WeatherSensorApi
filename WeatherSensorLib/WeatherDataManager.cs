using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Linq;

using Virtustream.WeatherSensorLib.Interfaces;
using Virtustream.WeatherSensorLib.ExternalApi;
using Virtustream.WeatherSensorLib.Configs;

namespace Virtustream.WeatherSensorLib
{
    public class WeatherDataManager : IWeatherDataManager
    {
        private readonly IWeatherDataAccess weatherDataRepo;
        private readonly WeatherDataManagerConfig config;

        public WeatherDataManager(IWeatherDataAccess weatherDataRepo)
        {
            this.weatherDataRepo = weatherDataRepo;
            config = new WeatherDataManagerConfig();    //Dependency inject would be better, but at the moment this is fine enough.
        }

        public List<DayWeatherData> GetWeatherData(ISensor sensor, int numberOfDays)
        {
            var days = new List<DateTime>();
            var currentDay = DateTime.Today;
            for (int i = 1; i <= numberOfDays; i++)
            {
                days.Add(currentDay.AddDays(i));
            }

            if (!weatherDataRepo.TryGetWeatherData(sensor.City, days, out List<DayWeatherData> cityWeatherData))
            {
                RetrieveWeatherDataFromApi(sensor.City, numberOfDays);
                weatherDataRepo.TryGetWeatherData(sensor.City, days, out cityWeatherData);
            }

            return cityWeatherData;
        }

        private void RetrieveWeatherDataFromApi(string city, int numberOfDays)
        {
            //Forecast API returns days counting from today so we want to increase the number of days by 1 to get all the next days.
            var url = config.WeatherForecastUrl(city, numberOfDays + 1);

            string responseString;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                var response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseString = reader.ReadToEnd();
            }
            catch
            {
                throw new Exception("Web API does not responde to a given URL: " + url);
            }

            var forecastData = JsonSerializer.Deserialize<ForecastApiResponse>(responseString, config.JsonSerializerOptions);

            var dayWeatherDatas = forecastData.forecast.forecastday.Select(x => new DayWeatherData(city, DateTime.Parse(x.date), x)).ToList();
            weatherDataRepo.StoreWeatherData(dayWeatherDatas);
        }
    }
}
