using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherEx2.Models
{
    public class UserCityItem
    {
        public List<UserCity> PossibleCities { get; set; }
        [Required]
        public string UserId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool FavoriteCity { get; set; }

        public UserCityItem()
        {
            PossibleCities = new List<UserCity>();
            UserId = "";
            CityId = -1;
            CityName = "";
            FavoriteCity = false;
        }

    }
}