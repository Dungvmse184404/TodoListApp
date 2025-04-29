using Microsoft.Extensions.Configuration;

namespace ApiExtension.Models.RequestModels
{
    public class WeatherRequestModel
    {
        public string City { get; set; } = null!;
        private string ApiKey { get; set; } = null!;

        public string GetRequestUrl()
        {
            var ApiKey = GetKey();
            if (ApiKey != null)
            {
                return $"https://api.openweathermap.org/data/2.5/weather?q={City}&appid={ApiKey}&units=metric&lang=en";
            }
            return "";
        }

        private string? GetKey()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("keys.json", true, true)
                        .Build();
            var strConn = config["WeatherApi"];

            return strConn;
        }
    }
}
