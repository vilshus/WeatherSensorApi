using System.Text.Json;
using System.IO;

namespace Virtustream.WeatherSensorLib.Configs
{
    /// <summary>
    /// 
    /// </summary>
    public class WeatherDataManagerConfig
    {
        /// <summary>
        /// User ID for weather forecast API.
        /// </summary>
        private ApiUserKey userKey;

        public string WeatherForecastUrl(string city, int numberOfDays) => $"http://api.weatherapi.com/v1/forecast.json?key={userKey.Key}&q={city}&days={numberOfDays}";

        public JsonSerializerOptions JsonSerializerOptions { get; set; }

        public WeatherDataManagerConfig()
        {
            JsonSerializerOptions = new JsonSerializerOptions();
            JsonSerializerOptions.PropertyNameCaseInsensitive = true;

            string userKeyJson;
            if (!File.Exists("ApiUserKey.json"))
            {
                userKey = new ApiUserKey();
                userKeyJson = JsonSerializer.Serialize(userKey);
                File.WriteAllText("ApiUserKey.json", userKeyJson);
            }
            else
            {
                userKeyJson = File.ReadAllText("ApiUserKey.json");
                userKey = JsonSerializer.Deserialize<ApiUserKey>(userKeyJson);
            }
        }
    }
}
