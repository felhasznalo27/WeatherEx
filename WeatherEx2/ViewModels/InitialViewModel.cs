using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherEx2.Models;
using WeatherEx2.Models.DataModels;

namespace WeatherEx2.ViewModels
{
    public class InitialViewModel
    {
        public List<UserCity> CityList { get; set; }
        public string Message { get; set; }
        public int ImportedCityCount { get; set; }
    }
}