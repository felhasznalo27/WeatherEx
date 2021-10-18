using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherEx2.Models
{
    public class UserCityRelation
    {
        public string UserId { get; set; }
        public int CityId { get; set; }
        public bool FavoriteCity { get; set; }
    }
}