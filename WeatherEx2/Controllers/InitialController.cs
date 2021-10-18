using System;
using System.Linq;
using System.Web.Mvc;
using WeatherEx2.Models.DataModels;
using WeatherEx2.ViewModels;

namespace WeatherEx2.Controllers
{
    public class InitialController : Controller
    {
        // GET: Initial/Initial
        public ActionResult Initial()
        {
            ImportBusinessLayer importer = new ImportBusinessLayer(new ImportDataAccessLayer());

            InitialViewModel viewModel = new InitialViewModel();

            try
            {
                viewModel.ImportedCityCount = importer.ImportedCityCount();

                if (viewModel.ImportedCityCount < 5)
                {
                    //string citiesResourcePath = HttpContext.Server.MapPath(@"\Resources\cities_all.csv");
                    string citiesResourcePath = HttpContext.Server.MapPath(@"\Resources\cities_20000.csv");

                    viewModel.CityList = importer.GetAllImpoetedCityFromCsv(citiesResourcePath);
                    viewModel.Message = "";

                    importer.SaveAllCityAndDeletePrevious(viewModel.CityList.Where(x => x.CountryCode == "HU").ToList());

                    var dbCityList = importer.GetAllImportedCity();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View(viewModel);
        }
    }
}