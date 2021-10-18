using System.Collections.Generic;
using WeatherEx2.Models;

namespace WeatherEx2.ViewModels
{
    public class UserCityViewModel
    {
        public string UserId { get; set; }
        public List<UserCity> UserCityList { get; set; }
    }
}