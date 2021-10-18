using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WeatherEx2.Models.DataModels
{
    public class ImportDataAccessLayer : IImport
    {
        public CurrentWeatherInfo GetCurrentWeatherInfo(UserCity city, string providerUrl, string apiKey)
        {
            String requestUrl = CurrentWeatherDefaultUrl(providerUrl, city.Lat, city.Lon, apiKey, "minutely", "hu");
            String jsonContent = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    jsonContent = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
            }
            catch (Exception ex)
            {
                string myMessage = "\r\nError in HttpWebRequest: " + requestUrl;
                throw new WebException("Error in GetCurrentWeatherInfo(). Message: " + ex.Message + myMessage, ex);
            }

            return GetWeatherDataFromJson(jsonContent, city.Id);
        }

        public string CurrentWeatherDefaultUrl(string providerUrl, double lat, double lon, string apiKey, string include, string lang)
        {
            return providerUrl + "?lat=" + lat.ToString("G", CultureInfo.InvariantCulture) + "&lon=" + lon.ToString("G", CultureInfo.InvariantCulture) + "&key=" + apiKey + "&inclue=" + include + "&lang=" + lang;
        }

        public CurrentWeatherInfo GetWeatherDataFromJson(string jsonContent, int cityId)
        {
            CurrentWeatherInfo currentWeather = new CurrentWeatherInfo();

            var mainData = JsonConvert.DeserializeObject<JObject>(jsonContent);
            var myData = mainData.GetValue("data");
            var myDataValues = JsonConvert.DeserializeObject<IList<JObject>>(myData.ToString());

            currentWeather.CityId = cityId;
            currentWeather.StationId = (string)myDataValues.First().GetValue("station");
            currentWeather.CurrentCycleHour = DateTime.ParseExact((string)myDataValues.First().GetValue("datetime"), "yyyy-MM-dd:HH", CultureInfo.InvariantCulture);
            currentWeather.LastObservationTime = DateTime.ParseExact((string)myDataValues.First().GetValue("ob_time"), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            currentWeather.SunriseTime = DateTime.ParseExact((string)myDataValues.First().GetValue("sunrise"), "HH:mm", CultureInfo.InvariantCulture);
            currentWeather.SunsetTime = DateTime.ParseExact((string)myDataValues.First().GetValue("sunset"), "HH:mm", CultureInfo.InvariantCulture);
            currentWeather.WeatherIcon = (string)((JObject)myDataValues.First().GetValue("weather")).GetValue("icon");
            currentWeather.WeatherCode = (int)((JObject)myDataValues.First().GetValue("weather")).GetValue("code");
            currentWeather.WeatherDescription = (string)((JObject)myDataValues.First().GetValue("weather")).GetValue("description");
            currentWeather.Temperature = (double)myDataValues.First().GetValue("temp");
            currentWeather.TemperatureFeelsLike = (double)myDataValues.First().GetValue("app_temp");
            currentWeather.Pressure = (double)myDataValues.First().GetValue("pres");
            currentWeather.LiquidPercipitationRate = (int)myDataValues.First().GetValue("precip");
            currentWeather.UvIndex = (int)myDataValues.First().GetValue("uv");
            currentWeather.RelativeHumidity = (int)myDataValues.First().GetValue("rh");

            return currentWeather;
        }

        public List<ForecastWeatherInfo> GetForecastWeatherInfo(UserCity city, string providerUrl, string apiKey)
        {
            String requestUrl = ForecastWeatherDefaultUrl(providerUrl, city.Lat, city.Lon, apiKey, "hu");
            String jsonContent = "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    jsonContent = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }
            }
            catch (Exception ex)
            {
                string myMessage = "\r\nError in HttpWebRequest: " + requestUrl;
                throw new WebException("Error in GetForecastWeatherInfo(). Message: " + ex.Message + myMessage, ex);
            }

            return GetForecastWeatherListFromJson(jsonContent, city.Id);
        }

        public string ForecastWeatherDefaultUrl(string providerUrl, double lat, double lon, string apiKey, string lang)
        {
            return providerUrl + "?lat=" + lat.ToString("G", CultureInfo.InvariantCulture) + "&lon=" + lon.ToString("G", CultureInfo.InvariantCulture) + "&key=" + apiKey + "&lang=" + lang;
        }

        public List<ForecastWeatherInfo> GetForecastWeatherListFromJson(string jsonContent, int cityId)
        {
            List<ForecastWeatherInfo> weatherInfoList = new List<ForecastWeatherInfo>();

            var mainData = JsonConvert.DeserializeObject<JObject>(jsonContent);
            var myData = mainData.GetValue("data");
            var myDataValues = JsonConvert.DeserializeObject<IList<JObject>>(myData.ToString());

            foreach (var value in myDataValues)
            {
                var tempWeatherInfo = new ForecastWeatherInfo();

                tempWeatherInfo.CityId = cityId;
                tempWeatherInfo.WeatherIcon = (string)((JObject)value.GetValue("weather")).GetValue("icon");
                tempWeatherInfo.WeatherCode = (int)((JObject)value.GetValue("weather")).GetValue("code");
                tempWeatherInfo.WeatherDescription = (string)((JObject)value.GetValue("weather")).GetValue("description");
                tempWeatherInfo.MinTemperature = (double)value.GetValue("min_temp");
                tempWeatherInfo.MaxTemperature = (double)value.GetValue("max_temp");
                tempWeatherInfo.WindSpeed = (double)value.GetValue("wind_spd");
                tempWeatherInfo.ValidDate = DateTime.ParseExact((string)value.GetValue("valid_date"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                tempWeatherInfo.LiquidAmount = (int)myDataValues.First().GetValue("precip");
                tempWeatherInfo.AveragePressure = (double)value.GetValue("pres");
                tempWeatherInfo.AverageHumidity = (double)value.GetValue("rh");
                tempWeatherInfo.AverageCloud = (int)myDataValues.First().GetValue("clouds");
                tempWeatherInfo.MaximumUv = (int)myDataValues.First().GetValue("uv");

                weatherInfoList.Add(tempWeatherInfo);
            }

            return weatherInfoList;
        }       

        
        public void DeleteAllImportedCity()
        {
            using (DataEntities db = new DataEntities())
            {
                var tempList = db.TImportedCity.ToList();

                db.TImportedCity.RemoveRange(tempList);

                db.SaveChanges();
            }
        }

        public int ImportedCityCount()
        {
            int cityCount = 0;
            using (DataEntities db = new DataEntities())
            {
                cityCount = db.TImportedCity.Count();
            }

            return cityCount;
        }

        public void SaveAllCityAndDeletePrevious(List<UserCity> cityList)
        {
            if (!cityList.Any())
            {
                string myMessage = "List has no element.";
                throw new ArgumentException("Error in SaveAllCity(). Message: " + myMessage);
            }

            DeleteAllImportedCity();

            using (DataEntities db = new DataEntities())
            {
                foreach (var city in cityList)
                {
                    db.TImportedCity.Add(CommonDataTranslator.AsSQLEntity(city));
                }

                db.SaveChanges();
            }
        }

        public List<TImportedCity> GetAllImportedCity()
        {
            List<TImportedCity> temp = new List<TImportedCity>();

            using (DataEntities db = new DataEntities())
            {
                temp = db.TImportedCity.ToList();
            }

            return temp;
        }

        public List<UserCity> GetCitiesFromCsv(string csvFilePath)
        {
            List<UserCity> cityList = new List<UserCity>();

            var data = GetDataTableFromCSVFile(csvFilePath);

            if (data.Rows.Count > 1)
            {

                foreach (DataRow row in data.Rows)
                {
                    string errorMessage = String.Empty;
                    bool hasValue = false;

                    string tempCityId = row[0].ToString();
                    string tempCityName = row[1].ToString();
                    string tempStateCode = row[2].ToString();
                    string tempCountryCode = row[3].ToString();
                    string tempCountryFull = row[4].ToString();
                    string tempLat = row[5].ToString();
                    string tempLon = row[6].ToString();


                    int cityId = 0;
                    int intOut = 0;
                    hasValue = Int32.TryParse(tempCityId, out intOut);
                    if (hasValue)
                    {
                        cityId = intOut;
                    }
                    else
                    {
                        errorMessage += "Invalid data (cityId): " + tempCityId + "\r\n";
                    }

                    double lat = 0;
                    double doubleOut = 0;
                    hasValue = double.TryParse(tempLat, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out doubleOut);
                    if (hasValue)
                    {
                        lat = doubleOut;
                    }
                    else
                    {
                        errorMessage += "Invalid data (lat): " + tempLat + "\r\n";
                    }

                    double lon = 0;
                    doubleOut = 0;
                    hasValue = double.TryParse(tempLon, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out doubleOut);
                    if (hasValue)
                    {
                        lon = doubleOut;
                    }
                    else
                    {
                        errorMessage += "Invalid data (lon): " + tempLon + "\r\n";
                    }

                    if (errorMessage == "")
                    {
                        var tempCity = new UserCity
                        {
                            CityId = cityId,
                            CityName = tempCityName,
                            StateCode = tempStateCode,
                            CountryCode = tempCountryCode,
                            CountryFull = tempCountryFull,
                            Lat = lat,
                            Lon = lon
                        };

                        cityList.Add(tempCity);
                    }
                    else
                    {
                        if (errorMessage != "")
                        {
                            throw new InvalidDataException("Error in GetCitiesFromCsv(). Message: " + errorMessage);
                        }
                    }
                }
            }

            return cityList;
        }

        public DataTable GetDataTableFromCSVFile(string csvFilePath)
        {
            DataTable csvData = new DataTable();

            try
            {
                using (Microsoft.VisualBasic.FileIO.TextFieldParser csvReader = new Microsoft.VisualBasic.FileIO.TextFieldParser(csvFilePath, Encoding.UTF8))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();

                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }

                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                string myMessage = "\r\nError reading file: " + csvFilePath;
                throw new ArgumentException("Error in GetDataTableFromCSVFile(). Message: " + ex.Message + myMessage, ex);
            }

            return csvData;
        }
    }
}