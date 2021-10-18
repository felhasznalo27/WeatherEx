using System.Collections.Generic;

namespace WeatherEx2.Models.DataModels
{
    public interface IImport
    {
        int ImportedCityCount();
        List<TImportedCity> GetAllImportedCity();
        CurrentWeatherInfo GetCurrentWeatherInfo(UserCity city, string providerUrl, string apiKey);
        List<ForecastWeatherInfo> GetForecastWeatherInfo(UserCity city, string providerUrl, string apiKey);
        List<UserCity> GetCitiesFromCsv(string csvFilePath);
        void SaveAllCityAndDeletePrevious(List<UserCity> cityList);
    }
}
