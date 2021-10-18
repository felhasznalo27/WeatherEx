using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherEx2.Models
{
    public class ForecastWeatherInfo : WeatherInfo
    {
        // min_temp: Minimum Temperature (default Celcius)
        public double MinTemperature { get; set; }
        // max_temp: Maximum Temperature (default Celcius) 
        public double MaxTemperature { get; set; }
        // wind_spd: Wind speed(Default m/s)
        public double WindSpeed { get; set; }
        // valid_date: Date the forecast is valid for in format YYYY-MM-DD[Midnight to midnight local time]
        public DateTime ValidDate { get; set; }
        // precip: Accumulated liquid equivalent precipitation(default mm)
        public int LiquidAmount { get; set; }
        // pres: Average pressure (mb)
        public double AveragePressure { get; set; }
        // rh: Average relative humidity (%)
        public double AverageHumidity { get; set; }
        // clouds: Average total cloud coverage(%)
        public int AverageCloud { get; set; }
        // uv: Maximum UV Index(0-11+)
        public int MaximumUv { get; set; }

        public ForecastWeatherInfo()
        {
            MinTemperature = 0;
            MaxTemperature = 0;
            WindSpeed = 0;
            ValidDate = DateTime.Now;
            LiquidAmount = 0;
            AveragePressure = 0;
            AverageHumidity = 0;
            AverageCloud = 0;
            MaximumUv = 0;
        }
    }
}