using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherEx2.Models
{
    public class WeatherInfo
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        // icon:Weather icon code.
        public string WeatherIcon { get; set; }
        // code:Weather code.
        public int WeatherCode { get; set; }
        // description: Text weather description.
        public string WeatherDescription { get; set; }

        public WeatherInfo()
        {
            Id = -1;
            CityId = -1;
            WeatherIcon = "";
            WeatherCode = 0;
            WeatherDescription = "";
        }
    }
}