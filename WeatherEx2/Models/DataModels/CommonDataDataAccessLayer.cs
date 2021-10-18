using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherEx2.Models.DataModels
{
    public class CommonDataDataAccessLayer : ICommonData
    {
        public int AvailableCityCount()
        {
            int cityCount = 0;
            using (DataEntities db = new DataEntities())
            {
                cityCount = db.TImportedCity.Count();
            }

            return cityCount;
        }

        public UserCity GetCityByCityId(int cityId)
        {
            UserCity city = new UserCity();

            using (DataEntities db = new DataEntities())
            {
                var tempCity = db.TImportedCity.Where(x => x.Id == cityId).FirstOrDefault();

                if (tempCity != null)
                {
                    city = CommonDataTranslator.AsEntity(tempCity);
                }
                else
                {
                    string myMessage = "City not found.";
                    throw new ArgumentException("Error in GetCityByCityId(). Message: " + myMessage);
                }
            }

            return city;
        }

        public UserCityItem GetUserCityRelationByCityId(string userId, int cityId)
        {
            UserCityItem userCityItem = new UserCityItem();

            userCityItem.UserId = userId;
            userCityItem.CityId = cityId;

            using (DataEntities db = new DataEntities())
            {
                var tempItem = db.TUser2City.Where(x => x.UserId == userId && x.CityId == cityId).FirstOrDefault();

                if (tempItem != null)
                {
                    userCityItem.FavoriteCity = tempItem.FavoriteCity;
                    userCityItem.CityName = tempItem.TImportedCity.CityName;
                }
            }

            return userCityItem;
        }

        public List<UserCity> GetAllUserCityByUserId(string userId)
        {
            List<UserCity> cityList = new List<UserCity>();

            using (DataEntities db = new DataEntities())
            {
                cityList = (from c in db.TUser2City
                            where c.UserId == userId
                            select new UserCity
                            {
                                Id = c.TImportedCity.Id,
                                CityId = c.TImportedCity.CityId,
                                CityName = c.TImportedCity.CityName,
                                StateCode = c.TImportedCity.StateCode,
                                CountryCode = c.TImportedCity.CountryCode,
                                CountryFull = c.TImportedCity.CountryFull,
                                Lat = c.TImportedCity.Lat,
                                Lon = c.TImportedCity.Lon
                            }).ToList();
            }

            return cityList;
        }

        public List<UserCity> GetAllPossibleUserCity()
        {
            List<UserCity> cityList = new List<UserCity>();

            using (DataEntities db = new DataEntities())
            {
                var tempCityList = db.TImportedCity.ToList();

                if (tempCityList.Any())
                {
                    foreach (var item in tempCityList)
                    {
                        var city = CommonDataTranslator.AsEntity(item);
                        cityList.Add(city);
                    }
                }
            }

            return cityList;
        }        

        public Dictionary<bool, CurrentWeatherInfo> GetCurrentWeatherInfoFromDatabaseByCityId(int cityId)
        {
            var weatherInfo = new Dictionary<bool, CurrentWeatherInfo>();

            bool isValid = false;
            CurrentWeatherInfo currentWeatherInfo = new CurrentWeatherInfo();

            using (DataEntities db = new DataEntities())
            {
                var tempWeatherInfo = db.TCurrentWeatherInfo.Where(x => x.CityId == cityId).ToList();

                if (tempWeatherInfo.Any())
                {
                    if (tempWeatherInfo.First().LastModified >= DateTime.Now.AddHours(-2))
                    {
                        isValid = true;
                        currentWeatherInfo = CommonDataTranslator.AsEntity(tempWeatherInfo.First());
                    }
                }
            }

            weatherInfo.Add(isValid, currentWeatherInfo);

            return weatherInfo;
        }

        public Dictionary<bool, List<ForecastWeatherInfo>> GetForecastWeatherInfoFromDatabaseByCityId(int cityId)
        {
            var weatherInfo = new Dictionary<bool, List<ForecastWeatherInfo>>();

            bool isValid = false;
            List<ForecastWeatherInfo> forecastWeatherInfoList = new List<ForecastWeatherInfo>();

            using (DataEntities db = new DataEntities())
            {
                var tempWeatherInfo = db.TForecastWeatherInfo.Where(x => x.CityId == cityId).ToList();

                if (tempWeatherInfo.Any())
                {
                    if (tempWeatherInfo.First().LastModified >= DateTime.Now.AddHours(-24))
                    {
                        isValid = true;

                        foreach (var item in tempWeatherInfo)
                        {
                            forecastWeatherInfoList.Add(CommonDataTranslator.AsEntity(item));
                        }
                    }
                }
            }

            weatherInfo.Add(isValid, forecastWeatherInfoList);

            return weatherInfo;
        }

        public void SaveUserCity(UserCityItem userCityRelation)
        {
            using (DataEntities db = new DataEntities())
            {
                // check before add
                var item = db.TUser2City.Where(x => x.UserId == userCityRelation.UserId && x.CityId == userCityRelation.CityId).FirstOrDefault();

                if (item == null)
                {
                    TUser2City relation = new TUser2City();
                    relation.UserId = userCityRelation.UserId;
                    relation.CityId = userCityRelation.CityId;
                    relation.FavoriteCity = userCityRelation.FavoriteCity;
                    relation.LastModified = DateTime.Now;

                    db.TUser2City.Add(relation);
                    db.SaveChanges();
                }
            }
        }

        public void SaveCurrentWeatherInfo(CurrentWeatherInfo currentWeatherInfo)
        {
            using (DataEntities db = new DataEntities())
            {
                // delete previous data befor save new
                var oldItems = db.TCurrentWeatherInfo.Where(x => x.CityId == currentWeatherInfo.CityId).ToList();

                if (oldItems.Any())
                {
                    db.TCurrentWeatherInfo.RemoveRange(oldItems);
                }

                db.SaveChanges();

                db.TCurrentWeatherInfo.Add(CommonDataTranslator.AsSQLEntity(currentWeatherInfo));
                db.SaveChanges();
            }
        }

        public void SaveForecastWeatherInfo(List<ForecastWeatherInfo> forecastWeatherInfoList)
        {
            using (DataEntities db = new DataEntities())
            {
                // delete previous data befor save new
                var cityId = forecastWeatherInfoList.First().CityId;

                var oldItems = db.TForecastWeatherInfo.Where(x => x.CityId == cityId).ToList();

                if (oldItems.Any())
                {
                    db.TForecastWeatherInfo.RemoveRange(oldItems);
                }

                db.SaveChanges();

                foreach (var forecastWeatherInfo in forecastWeatherInfoList)
                {
                    db.TForecastWeatherInfo.Add(CommonDataTranslator.AsSQLEntity(forecastWeatherInfo));
                }

                db.SaveChanges();
            }
        }

        public void UpdateUserCity(UserCityItem userCityRelation)
        {
            using (DataEntities db = new DataEntities())
            {
                var relation = db.TUser2City.Where(x => x.UserId == userCityRelation.UserId && x.CityId == userCityRelation.CityId).FirstOrDefault();
                relation.FavoriteCity = userCityRelation.FavoriteCity;
                relation.LastModified = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void DeleteUserCityByCityId(string userId, int cityId)
        {
            using (DataEntities db = new DataEntities())
            {
                // delete weather data
                var currentWeatherOldItems = db.TCurrentWeatherInfo.Where(x => x.CityId == cityId).ToList();

                if (currentWeatherOldItems.Any())
                {
                    db.TCurrentWeatherInfo.RemoveRange(currentWeatherOldItems);
                    db.SaveChanges();
                }



                var forecastWeatherOldItems = db.TForecastWeatherInfo.Where(x => x.CityId == cityId).ToList();

                if (forecastWeatherOldItems.Any())
                {
                    db.TForecastWeatherInfo.RemoveRange(forecastWeatherOldItems);
                    db.SaveChanges();
                }

                // remove city
                var item = db.TUser2City.Where(x => x.UserId == userId && x.CityId == cityId).FirstOrDefault();
                db.TUser2City.Remove(item);
                db.SaveChanges();
            }
        }
    }
}