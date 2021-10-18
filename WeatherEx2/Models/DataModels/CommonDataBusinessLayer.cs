using System.Collections.Generic;

namespace WeatherEx2.Models.DataModels
{
    public class CommonDataBusinessLayer
    {
        public ICommonData _commonData;
        public CommonDataBusinessLayer(ICommonData commonData)
        {
            _commonData = commonData;
        }

        public int AvailableCityCount()
        {
            return _commonData.AvailableCityCount();
        }

        public List<UserCity> GetAllPossibleUserCity()
        {
            return _commonData.GetAllPossibleUserCity();
        }

        public List<UserCity> GetAllUserCityByUserId(string userId)
        {
            return _commonData.GetAllUserCityByUserId(userId);
        }

        public UserCity GetCityByCityId(int cityId)
        {
            return _commonData.GetCityByCityId(cityId);
        }

        public UserCityItem GetUserCityRelationByCityId(string userId, int cityId)
        {
            return _commonData.GetUserCityRelationByCityId(userId, cityId);
        }

        public Dictionary<bool, CurrentWeatherInfo> GetCurrentWeatherInfoFromDatabaseByCityId(int cityId)
        {
            return _commonData.GetCurrentWeatherInfoFromDatabaseByCityId(cityId);
        }

        public Dictionary<bool, List<ForecastWeatherInfo>> GetForecastWeatherInfoFromDatabaseByCityId(int cityId)
        {
            return _commonData.GetForecastWeatherInfoFromDatabaseByCityId(cityId);
        }

        public void SaveCurrentWeatherInfo(CurrentWeatherInfo currentWeatherInfo)
        {
            _commonData.SaveCurrentWeatherInfo(currentWeatherInfo);
        }
        public void SaveForecastWeatherInfo(List<ForecastWeatherInfo> forecastWeatherInfoList)
        {
            _commonData.SaveForecastWeatherInfo(forecastWeatherInfoList);
        }

        public void SaveUserCity(UserCityItem userCityRelation)
        {
            _commonData.SaveUserCity(userCityRelation);
        }
        public void UpdateUserCity(UserCityItem userCityRelation)
        {
            _commonData.UpdateUserCity(userCityRelation);
        }
        public void DeleteUserCityByCityId(string userId, int cityId)
        {
            _commonData.DeleteUserCityByCityId(userId, cityId);
        }
    }
}