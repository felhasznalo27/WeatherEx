using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using WeatherEx2.Models;
using WeatherEx2.Models.DataModels;
using WeatherEx2.ViewModels;

namespace WeatherEx2.Controllers
{
    public class UserCityController : Controller
    {
        CommonDataBusinessLayer _commonData;
        ImportBusinessLayer _importer;

        public UserCityController()
        {
            _commonData = new CommonDataBusinessLayer(new CommonDataDataAccessLayer());
            _importer = new ImportBusinessLayer(new ImportDataAccessLayer());
        }
        // GET: UserCity/UserCity
        public ActionResult UserCity()
        {
            UserCityViewModel viewModel = new UserCityViewModel();

            viewModel.UserId = User.Identity.GetUserId();

            // login please
            if (viewModel.UserId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            viewModel.UserCityList = _commonData.GetAllUserCityByUserId(viewModel.UserId);
            viewModel.UserCityList = viewModel.UserCityList.OrderBy(x => x.CityName).ToList();

            return View(viewModel);
        }

        // GET: UserCity/AddNewCity
        public ActionResult AddNewCity()
        {
            var userId = User.Identity.GetUserId();

            // login please
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            UserCityItem viewModel = new UserCityItem();

            viewModel.PossibleCities = _commonData.GetAllPossibleUserCity();
            var userCityList = _commonData.GetAllUserCityByUserId(userId);

            if (userCityList.Any())
            {
                var userIdList = userCityList.Select(x => x.CityId).ToList();
                viewModel.PossibleCities = viewModel.PossibleCities.Where(x => userIdList.IndexOf(x.CityId) == -1).ToList();
                viewModel.PossibleCities = viewModel.PossibleCities.OrderBy(x => x.CityName).ToList();
            }

            return View(viewModel);
        }

        public ActionResult DeleteCity(int id)
        {
            var userId = User.Identity.GetUserId();

            // login please
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            _commonData.DeleteUserCityByCityId(userId, id);

            return RedirectToAction("UserCity", "UserCity");
        }

        public ActionResult EditCity(int id)
        {
            UserCityItem viewData = new UserCityItem();

            var userId = User.Identity.GetUserId();

            // login please
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            viewData = _commonData.GetUserCityRelationByCityId(userId, id);

            return View("EditCity", viewData);
        }

        // POST
        public ActionResult UpdateUserCity(UserCityItem userCityItem)
        {
            var userId = User.Identity.GetUserId();

            // login please
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                UserCityItem data = _commonData.GetUserCityRelationByCityId(userId, userCityItem.CityId);
                data.FavoriteCity = userCityItem.FavoriteCity;
                _commonData.UpdateUserCity(data);
            }

            return RedirectToAction("UserCity", "UserCity");
        }

        public ActionResult CityDetails(int id)
        {
            var userId = User.Identity.GetUserId();

            // login please
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                string apiKey = WebConfigurationManager.AppSettings["WeatherbitApiKey"];
                string providerUrlCurrentWeather = WebConfigurationManager.AppSettings["CurrentWeatherServiceProviderUrl"];
                string providerUrlForecastWeather = WebConfigurationManager.AppSettings["ForecastWeatherServiceProviderUrl"];

                CityDetailsViewModel viewModel = new CityDetailsViewModel();

                viewModel.City = _commonData.GetCityByCityId(id);

                Dictionary<bool, CurrentWeatherInfo> dbCurrentWeatherInfo = _commonData.GetCurrentWeatherInfoFromDatabaseByCityId(id);

                // has valid data in db?
                if (dbCurrentWeatherInfo.First().Key)
                {
                    // show data from db
                    viewModel.CurrentWeather = dbCurrentWeatherInfo.First().Value;
                }
                else
                {
                    // use api reuest
                    viewModel.CurrentWeather = _importer.GetCurrentWeatherInfo(viewModel.City, providerUrlCurrentWeather, apiKey);
                    _commonData.SaveCurrentWeatherInfo(viewModel.CurrentWeather);
                }

                Dictionary<bool, List<ForecastWeatherInfo>> dbForecastWeatherInfo = _commonData.GetForecastWeatherInfoFromDatabaseByCityId(id);

                // has valid data in db?
                if (dbForecastWeatherInfo.First().Key)
                {
                    // show data from db
                    viewModel.ForecastWeatherList = dbForecastWeatherInfo.First().Value.Where(x => x.ValidDate > DateTime.Now).Take(3).OrderBy(x => x.ValidDate).ToList();
                }
                else
                {
                    // use api reuest
                    viewModel.ForecastWeatherList = _importer.GetForecastWeatherInfo(viewModel.City, providerUrlForecastWeather, apiKey);
                    _commonData.SaveForecastWeatherInfo(viewModel.ForecastWeatherList);

                    viewModel.ForecastWeatherList = viewModel.ForecastWeatherList.Where(x => x.ValidDate > DateTime.Now).Take(3).OrderBy(x => x.ValidDate).ToList();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "UserCity", new { message = "\"" + ex.Message + "\"" });
            }
        }

        /// <summary>
        /// Saving the given city
        /// </summary>
        /// <param name="userCityItem"></param>
        /// <returns></returns>
        public ActionResult SaveNewCity(UserCityItem userCityItem)
        {
            userCityItem.UserId = User.Identity.GetUserId();

            // login please
            if (userCityItem.UserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                _commonData.SaveUserCity(userCityItem);
            }

            return RedirectToAction("UserCity", "UserCity");
        }

        public ActionResult Index()
        {
            return RedirectToAction("UserCity", "UserCity");
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}