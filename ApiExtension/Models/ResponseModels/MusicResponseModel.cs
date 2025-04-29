using Newtonsoft.Json;

namespace ApiExtension.Models.ResponseModels
{
    public class MusicResponseModel
    {
        [JsonProperty("results")]
        public List<MusicInfo> ListTracks { get; set; } = null!;
    }

    public class MusicInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; } = null!;
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("artist_name")]
        public string ArtistName { get; set; } = null!;
        [JsonProperty("audio")]
        public string Audio { get; set; } = null!;
        [JsonProperty("image")]
        public string Image { get; set; } = null!;

        public override string? ToString()
        {
            return $"Name: {Name}";
        }
    }
}
