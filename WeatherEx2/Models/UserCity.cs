using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherEx2.Models
{
    public class UserCity
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string StateCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryFull { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}