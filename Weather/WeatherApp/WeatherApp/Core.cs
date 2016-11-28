using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string cityName)
        {
            //Sign up for a free API key at http://openweathermap.org/appid
            string key = "[Your API Key]";
            //string queryString = "http://api.openweathermap.org/data/2.5/weather?zip="
            //    + cityName + ",us&appid=" + key + "&units=imperial";
			
			// query the weather by city name				
			string queryString = "http://api.openweathermap.org/data/2.5/weather?q="
                + cityName + ",cn&appid=" + key + "&units=imperial";
				
            var results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                //weather.Temperature = (string)results["main"]["temp"] + " F";

                //for chinese should convert the F to C
                Double temp = (Double)((Convert.ToDouble(results["main"]["temp"]) - 32) / 1.8);

                //weather.Temperature = Convert.ToString(temp) + " C";
                weather.Temperature = temp.ToString("f2") + " C";

                weather.Wind = (string)results["wind"]["speed"] + " mph";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}