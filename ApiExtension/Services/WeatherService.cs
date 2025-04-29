using ApiExtension.Models.RequestModels;
using ApiExtension.Models.ResponseModels;
using Newtonsoft.Json;

namespace ApiExtension.Services
{
    public class WeatherService
    {
        public async Task<WeatherResponseModel?> GetCurrentWeather(WeatherRequestModel weatherRequestModel)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(weatherRequestModel.GetRequestUrl());

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                var weatherData = JsonConvert.DeserializeObject<WeatherResponseModel>(json);

                return weatherData;
            }

            return null;
        }

        public async Task<byte[]?> GetWeatherIconBytesAsync(string iconCode)
        {
            string url = $"https://openweathermap.org/img/wn/{iconCode}@2x.png";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                return null;
            }

        }
    }
}
