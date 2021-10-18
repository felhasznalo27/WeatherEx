using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherEx2.Models
{
    public class CurrentWeatherInfo : WeatherInfo
    {
        // station: Source station ID. 
        public string StationId { get; set; }
        //datetime: Current cycle hour(YYYY-MM-DD:HH). 
        public DateTime CurrentCycleHour { get; set; }
        //ob_time: Last observation time(YYYY-MM-DD HH:MM).
        public DateTime LastObservationTime { get; set; }
        // sunrise: Sunrise time (HH:MM). 
        public DateTime SunriseTime { get; set; }
        // sunset: Sunset time(HH:MM). 
        public DateTime SunsetTime { get; set; }
        // temp: Temperature(default Celcius). 
        public double Temperature { get; set; }
        // app_temp: Apparent/"Feels Like" temperature (default Celcius). 
        public double TemperatureFeelsLike { get; set; }
        // pres: Pressure (mb).
        public double Pressure { get; set; }
        // precip: Liquid equivalent precipitation rate(default mm/hr). 
        public int LiquidPercipitationRate { get; set; }
        // uv: UV Index (0-11+). 
        public int UvIndex { get; set; }
        // rh: Relative humidity(%).
        public int RelativeHumidity { get; set; }

        public CurrentWeatherInfo()
        {
            StationId = "";
            CurrentCycleHour = DateTime.Now;
            LastObservationTime = DateTime.Now;
            SunriseTime = DateTime.Now;
            SunsetTime = DateTime.Now;
            Temperature = 0;
            TemperatureFeelsLike = 0;
            Pressure = 0;
            LiquidPercipitationRate = 0;
            UvIndex = 0;
            RelativeHumidity = 0;
        }
    }
}