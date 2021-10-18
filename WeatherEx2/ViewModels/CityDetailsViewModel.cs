using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherEx2.Models;
using WeatherEx2.Models.DataModels;

namespace WeatherEx2.ViewModels
{
    public class CityDetailsViewModel
    {
        public UserCity City { get; set; }
        public CurrentWeatherInfo CurrentWeather { get; set; }
        public List<ForecastWeatherInfo> ForecastWeatherList { get; set; }
    }
}