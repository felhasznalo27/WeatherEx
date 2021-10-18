using System.Collections.Generic;

namespace WeatherEx2.Models.DataModels
{
    public interface ICommonData
    {
        int AvailableCityCount();
        List<UserCity> GetAllPossibleUserCity();
        List<UserCity> GetAllUserCityByUserId(string userId);
        UserCity GetCityByCityId(int cityId);
        UserCityItem GetUserCityRelationByCityId(string userId, int cityId);
        Dictionary<bool, CurrentWeatherInfo> GetCurrentWeatherInfoFromDatabaseByCityId(int cityId);
        Dictionary<bool, List<ForecastWeatherInfo>> GetForecastWeatherInfoFromDatabaseByCityId(int cityId);
        void SaveUserCity(UserCityItem userCityRelation);        
        void SaveCurrentWeatherInfo(CurrentWeatherInfo currentWeatherInfo);
        void SaveForecastWeatherInfo(List<ForecastWeatherInfo> forecastWeatherInfoList);
        void UpdateUserCity(UserCityItem userCityRelation);
        void DeleteUserCityByCityId(string userId, int cityId);        
    }
}
