using System.Collections.Generic;

namespace WeatherEx2.Models.DataModels
{
    public class ImportBusinessLayer
    {
        public IImport _importer;
        public ImportBusinessLayer(IImport import)
        {
            _importer = import;
        }

        public int ImportedCityCount()
        {
            return _importer.ImportedCityCount();
        }

        public List<TImportedCity> GetAllImportedCity()
        {
            return _importer.GetAllImportedCity();
        }
        public CurrentWeatherInfo GetCurrentWeatherInfo(UserCity city, string providerUrl, string apiKey)
        {
            return _importer.GetCurrentWeatherInfo(city, providerUrl, apiKey);
        }

        public List<ForecastWeatherInfo> GetForecastWeatherInfo(UserCity city, string providerUrl, string apiKey)
        {
            return _importer.GetForecastWeatherInfo(city, providerUrl, apiKey);
        }
        public List<UserCity> GetAllImpoetedCityFromCsv(string csvFilePath)
        {
            return _importer.GetCitiesFromCsv(csvFilePath);
        }

        public void SaveAllCityAndDeletePrevious(List<UserCity> cityList)
        {
            _importer.SaveAllCityAndDeletePrevious(cityList);
        }

    }
}