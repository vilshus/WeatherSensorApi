# WeatherSensorApi
A simple API for creation and management of virtual weather sensors and reporting of weather forecasts.

# User guide
## Startup
You just need to download the solution, build and launch it via VisualStudio IIS Express.

## Sensor management
One sensor is created on startup: <br/>
  * ID: 00000000-0000-0000-0000-000000000000
  * Name: Kaunas Sensor 1
  * City: Kaunas
  
### Create sensor
Send PUT request: <nowiki>https://localhost:44342/weathersensors/sensor?name=<sensor_name>&city=<sensor_city_name></nowiki>

### Update sensor
Send PUT request: <nowiki>https://localhost:44342/weathersensors/sensor?id=<sensor_guid>&name=<new_sensor_name>&city=<new_city_name></nowiki>

### Get sensor
Get specific sensor by ID:
* Send GET request: <nowiki>https://localhost:44342/weathersensors/sensor?id=<sensor_guid></nowiki>

Get all sensors:
* Send GET request: <nowiki>https://localhost:44342/weathersensors/sensor</nowiki>

### Delete sensor
Send DELETE request: <nowiki>https://localhost:44342/weathersensors/sensor?id=<sensor_guid></nowiki>

## Weather forecast
You can retrieve weather forecast for the given sensor. Forecast is returned for the city which is identified by the given sensor. <br/>
Retrieve forecast for number_days days: 
* Send GET request: <nowiki>https://localhost:44342/weathersensors/sensordata?id=<sensor_guid>&days=<number_days></nowiki>

If number_days is not provided or 0 then forecast for 2 days is returned.
