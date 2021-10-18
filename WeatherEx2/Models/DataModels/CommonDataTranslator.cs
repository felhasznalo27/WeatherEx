using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherEx2.Models.DataModels
{
    public static class CommonDataTranslator
    {
        public static UserCity AsEntity(TImportedCity city)
        {
            var tempCity = new UserCity();
            tempCity.Id = city.Id;
            tempCity.CityId = city.CityId;
            tempCity.CityName = city.CityName;
            tempCity.StateCode = city.StateCode;
            tempCity.CountryCode = city.CountryCode;
            tempCity.CountryFull = city.CountryFull;
            tempCity.Lat = city.Lat;
            tempCity.Lon = city.Lon;

            return tempCity;
        }

        public static TImportedCity AsSQLEntity(UserCity city)
        {
            var tempCity = new TImportedCity();
            tempCity.CityId = city.CityId;
            tempCity.CityName = city.CityName;
            tempCity.StateCode = city.StateCode;
            tempCity.CountryCode = city.CountryCode;
            tempCity.CountryFull = city.CountryFull;
            tempCity.Lat = city.Lat;
            tempCity.Lon = city.Lon;
            tempCity.LastModified = DateTime.Now;

            return tempCity;
        }

        public static TCurrentWeatherInfo AsSQLEntity(CurrentWeatherInfo currentWeatherInfo)
        {
            var tempWeatherInfo = new TCurrentWeatherInfo();
            tempWeatherInfo.CityId = currentWeatherInfo.CityId;
            tempWeatherInfo.WeatherIcon = currentWeatherInfo.WeatherIcon;
            tempWeatherInfo.WeatherCode = currentWeatherInfo.WeatherCode;
            tempWeatherInfo.WeatherDescription = currentWeatherInfo.WeatherDescription;
            tempWeatherInfo.StationId = currentWeatherInfo.StationId;
            tempWeatherInfo.CurrentCycleHour = currentWeatherInfo.CurrentCycleHour;
            tempWeatherInfo.LastObservationTime = currentWeatherInfo.LastObservationTime;
            tempWeatherInfo.SunriseTime = currentWeatherInfo.SunriseTime;
            tempWeatherInfo.SunsetTime = currentWeatherInfo.SunsetTime;
            tempWeatherInfo.Temperature = currentWeatherInfo.Temperature;
            tempWeatherInfo.TemperatureFeelsLike = currentWeatherInfo.TemperatureFeelsLike;
            tempWeatherInfo.Pressure = currentWeatherInfo.Pressure;
            tempWeatherInfo.LiquidPercipitationRate = currentWeatherInfo.LiquidPercipitationRate;
            tempWeatherInfo.UvIndex = currentWeatherInfo.UvIndex;
            tempWeatherInfo.RelativeHumidity = currentWeatherInfo.RelativeHumidity;
            tempWeatherInfo.LastModified = DateTime.Now;

            return tempWeatherInfo;
        }

        public static CurrentWeatherInfo AsEntity(TCurrentWeatherInfo currentWeatherInfo)
        {
            var tempWeatherInfo = new CurrentWeatherInfo();
            tempWeatherInfo.Id = currentWeatherInfo.Id;
            tempWeatherInfo.CityId = currentWeatherInfo.CityId;
            tempWeatherInfo.WeatherIcon = currentWeatherInfo.WeatherIcon;
            tempWeatherInfo.WeatherCode = currentWeatherInfo.WeatherCode;
            tempWeatherInfo.WeatherDescription = currentWeatherInfo.WeatherDescription;
            tempWeatherInfo.StationId = currentWeatherInfo.StationId;
            tempWeatherInfo.CurrentCycleHour = currentWeatherInfo.CurrentCycleHour;
            tempWeatherInfo.LastObservationTime = currentWeatherInfo.LastObservationTime;
            tempWeatherInfo.SunriseTime = currentWeatherInfo.SunriseTime;
            tempWeatherInfo.SunsetTime = currentWeatherInfo.SunsetTime;
            tempWeatherInfo.Temperature = currentWeatherInfo.Temperature;
            tempWeatherInfo.TemperatureFeelsLike = currentWeatherInfo.TemperatureFeelsLike;
            tempWeatherInfo.Pressure = currentWeatherInfo.Pressure;
            tempWeatherInfo.LiquidPercipitationRate = currentWeatherInfo.LiquidPercipitationRate;
            tempWeatherInfo.UvIndex = currentWeatherInfo.UvIndex;
            tempWeatherInfo.RelativeHumidity = currentWeatherInfo.RelativeHumidity;

            return tempWeatherInfo;
        }

        public static TForecastWeatherInfo AsSQLEntity(ForecastWeatherInfo forecastWeatherInfo)
        {
            var tempWeatherInfo = new TForecastWeatherInfo();
            tempWeatherInfo.CityId = forecastWeatherInfo.CityId;
            tempWeatherInfo.WeatherIcon = forecastWeatherInfo.WeatherIcon;
            tempWeatherInfo.WeatherCode = forecastWeatherInfo.WeatherCode;
            tempWeatherInfo.WeatherDescription = forecastWeatherInfo.WeatherDescription;
            tempWeatherInfo.MinTemperature = forecastWeatherInfo.MinTemperature;
            tempWeatherInfo.MaxTemperature = forecastWeatherInfo.MaxTemperature;
            tempWeatherInfo.WindSpeed = forecastWeatherInfo.WindSpeed;
            tempWeatherInfo.ValidDate = forecastWeatherInfo.ValidDate;
            tempWeatherInfo.LiquidAmount = forecastWeatherInfo.LiquidAmount;
            tempWeatherInfo.AveragePressure = forecastWeatherInfo.AveragePressure;
            tempWeatherInfo.AverageHumidity = forecastWeatherInfo.AverageHumidity;
            tempWeatherInfo.AverageCloud = forecastWeatherInfo.AverageCloud;
            tempWeatherInfo.MaximumUv = forecastWeatherInfo.MaximumUv;
            tempWeatherInfo.LastModified = DateTime.Now;

            return tempWeatherInfo;
        }

        public static ForecastWeatherInfo AsEntity(TForecastWeatherInfo forecastWeatherInfo)
        {
            var tempWeatherInfo = new ForecastWeatherInfo();
            tempWeatherInfo.Id = forecastWeatherInfo.Id;
            tempWeatherInfo.CityId = forecastWeatherInfo.CityId;
            tempWeatherInfo.WeatherIcon = forecastWeatherInfo.WeatherIcon;
            tempWeatherInfo.WeatherCode = forecastWeatherInfo.WeatherCode;
            tempWeatherInfo.WeatherDescription = forecastWeatherInfo.WeatherDescription;
            tempWeatherInfo.MinTemperature = forecastWeatherInfo.MinTemperature;
            tempWeatherInfo.MaxTemperature = forecastWeatherInfo.MaxTemperature;
            tempWeatherInfo.WindSpeed = forecastWeatherInfo.WindSpeed;
            tempWeatherInfo.ValidDate = forecastWeatherInfo.ValidDate;
            tempWeatherInfo.LiquidAmount = forecastWeatherInfo.LiquidAmount;
            tempWeatherInfo.AveragePressure = forecastWeatherInfo.AveragePressure;
            tempWeatherInfo.AverageHumidity = forecastWeatherInfo.AverageHumidity;
            tempWeatherInfo.AverageCloud = forecastWeatherInfo.AverageCloud;
            tempWeatherInfo.MaximumUv = forecastWeatherInfo.MaximumUv;

            return tempWeatherInfo;
        }
    }
}