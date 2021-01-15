using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;

namespace Weather_App
{
    public partial class MainPage : ContentPage
    {
        HttpClient client = new HttpClient();

        Location location = new Location();

        public MainPage()
        {
            InitializeComponent();
            GetLocation();
            APICall();
        }

        public async void GetLocation()
        {
            var tempLocation = await Geolocation.GetLastKnownLocationAsync();

            
            if (tempLocation != null)
            {
                location = tempLocation;
                Console.WriteLine(location);
            }
            else
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var newLocation = await Geolocation.GetLocationAsync(request);

                if (newLocation != null)
                {
                    location = newLocation;
                    Console.WriteLine(location);
                }
            }

        }
        public static DateTime GiveDate(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        private async void APICall()
        {
            string text = await client.GetStringAsync($"https://api.openweathermap.org/data/2.5/onecall?lat={location.Latitude}&lon={location.Longitude}&units=metric&exclude=minutely,hourly,alerts&appid=8eabbc6a1550705ef133b808b6b8bc2e");

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(text);

            if (data != null) {
                //Current Day
                Image.Source = $"https://openweathermap.org/img/wn/{data.Current.Weather[0].Icon}@2x.png";
                currentTemp.Text = $"{data.Current.Temp}°";
                humidityTemp.Text = $"{data.Current.Humidity.ToString()}%";
                UVTemp.Text = data.Current.Uvi.ToString();
                var LLVis = data.Current.Visibility / 1000;
                visibilityTemp.Text = $"{LLVis.ToString()}km";
                dewTemp.Text = $"{data.Current.DewPoint.ToString()}°";
                currentDay.Text = GiveDate(data.Current.Dt).DayOfWeek.ToString();
                

                //Day1
                day1.Text = GiveDate(data.Daily[2].Dt).DayOfWeek.ToString();
                day1Stats.Text = $"{data.Daily[2].Temp.Day}° / {data.Daily[2].Temp.Night}°";
                day1Image.Source = $"https://openweathermap.org/img/wn/{data.Daily[2].Weather[0].Icon}@2x.png";
                //Day2
                day2.Text = GiveDate(data.Daily[3].Dt).DayOfWeek.ToString();
                day2Stats.Text = $"{data.Daily[3].Temp.Day}° / {data.Daily[3].Temp.Night}°";
                day2Image.Source = $"https://openweathermap.org/img/wn/{data.Daily[3].Weather[0].Icon}@2x.png";
                //Day3
                day3.Text = GiveDate(data.Daily[4].Dt).DayOfWeek.ToString();
                day3Stats.Text = $"{data.Daily[4].Temp.Day}° / {data.Daily[4].Temp.Night}°";
                day3Image.Source = $"https://openweathermap.org/img/wn/{data.Daily[4].Weather[0].Icon}@2x.png";
                //Day4
                day4.Text = GiveDate(data.Daily[5].Dt).DayOfWeek.ToString();
                day4Stats.Text = $"{data.Daily[5].Temp.Day}° / {data.Daily[3].Temp.Night}°";
                day4Image.Source = $"https://openweathermap.org/img/wn/{data.Daily[5].Weather[0].Icon}@2x.png";
            }

        }

        public class Weather
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("main")]
            public string Main { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }
        }

        public class Current
        {
            [JsonProperty("dt")]
            public int Dt { get; set; }

            [JsonProperty("sunrise")]
            public int Sunrise { get; set; }

            [JsonProperty("sunset")]
            public int Sunset { get; set; }

            [JsonProperty("temp")]
            public double Temp { get; set; }

            [JsonProperty("feels_like")]
            public double FeelsLike { get; set; }

            [JsonProperty("pressure")]
            public int Pressure { get; set; }

            [JsonProperty("humidity")]
            public int Humidity { get; set; }

            [JsonProperty("dew_point")]
            public double DewPoint { get; set; }

            [JsonProperty("uvi")]
            public double Uvi { get; set; }

            [JsonProperty("clouds")]
            public int Clouds { get; set; }

            [JsonProperty("visibility")]
            public int Visibility { get; set; }

            [JsonProperty("wind_speed")]
            public double WindSpeed { get; set; }

            [JsonProperty("wind_deg")]
            public int WindDeg { get; set; }

            [JsonProperty("weather")]
            public List<Weather> Weather { get; set; }
        }

        public class Temp
        {
            [JsonProperty("day")]
            public double Day { get; set; }

            [JsonProperty("min")]
            public double Min { get; set; }

            [JsonProperty("max")]
            public double Max { get; set; }

            [JsonProperty("night")]
            public double Night { get; set; }

            [JsonProperty("eve")]
            public double Eve { get; set; }

            [JsonProperty("morn")]
            public double Morn { get; set; }
        }

        public class FeelsLike
        {
            [JsonProperty("day")]
            public double Day { get; set; }

            [JsonProperty("night")]
            public double Night { get; set; }

            [JsonProperty("eve")]
            public double Eve { get; set; }

            [JsonProperty("morn")]
            public double Morn { get; set; }
        }

        public class Daily
        {
            [JsonProperty("dt")]
            public int Dt { get; set; }

            [JsonProperty("sunrise")]
            public int Sunrise { get; set; }

            [JsonProperty("sunset")]
            public int Sunset { get; set; }

            [JsonProperty("temp")]
            public Temp Temp { get; set; }

            [JsonProperty("feels_like")]
            public FeelsLike FeelsLike { get; set; }

            [JsonProperty("pressure")]
            public int Pressure { get; set; }

            [JsonProperty("humidity")]
            public int Humidity { get; set; }

            [JsonProperty("dew_point")]
            public double DewPoint { get; set; }

            [JsonProperty("wind_speed")]
            public double WindSpeed { get; set; }

            [JsonProperty("wind_deg")]
            public int WindDeg { get; set; }

            [JsonProperty("weather")]
            public List<Weather> Weather { get; set; }

            [JsonProperty("clouds")]
            public int Clouds { get; set; }

            [JsonProperty("pop")]
            public int Pop { get; set; }

            [JsonProperty("uvi")]
            public double Uvi { get; set; }

            [JsonProperty("rain")]
            public double? Rain { get; set; }
        }

        public class Root
        {
            [JsonProperty("lat")]
            public double Lat { get; set; }

            [JsonProperty("lon")]
            public double Lon { get; set; }

            [JsonProperty("timezone")]
            public string Timezone { get; set; }

            [JsonProperty("timezone_offset")]
            public int TimezoneOffset { get; set; }

            [JsonProperty("current")]
            public Current Current { get; set; }

            [JsonProperty("daily")]
            public List<Daily> Daily { get; set; }
        }

    }
}
