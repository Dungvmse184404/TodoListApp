using Microsoft.Extensions.Configuration;

namespace ApiExtension.Models.RequestModels
{
    public class MusicRequestModel
    {
        private string ApiKey { get; set; } = null!;
        public int OffSet { get; set; }
        public string Tag { get; set; } = "lofi";

        public string GetRequestUrl()
        {
            var ApiKey = GetKey();
            if (ApiKey != null)
            {
                return $"https://api.jamendo.com/v3.0/tracks/?client_id={ApiKey}&format=json&tags=lofi&offset={OffSet}&order=popularity_total&audioformat=mp32";
            }
            return "";
        }

        private string? GetKey()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("keys.json", true, true)
                        .Build();
            var strConn = config["MusicApi"];

            return strConn;
        }
    }
}
