using Newtonsoft.Json;

namespace ApiExtension.Models.ResponseModels
{
    public class WeatherResponseModel
    {
        [JsonProperty("main")]
        public MainInfo MainInfo { get; set; }
        [JsonProperty("weather")]
        public Weather[] Weathers { get; set; }

        public override string? ToString()
        {
            return $"{MainInfo.Temp} | {Weathers[0].Description}";
        }
    }

    public class MainInfo
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; } = null!;
        [JsonProperty("icon")]
        public string Icon { get; set; } = null!;
    }
}
