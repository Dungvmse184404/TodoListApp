using ApiExtension.Models.RequestModels;
using ApiExtension.Models.ResponseModels;
using Newtonsoft.Json;

namespace ApiExtension.Services
{
    public class MusicService
    {
        public async Task<MusicResponseModel?> GetMusicTracks(int offSet, string tag)
        {
            MusicRequestModel request = new MusicRequestModel()
            {
                OffSet = offSet,
                Tag = tag
            };

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(request.GetRequestUrl());

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<MusicResponseModel>(json);
            }

            return null;
        }

        public async Task<MusicResponseModel?> GetRandomMusicTracks(string tag)
        {
            Random random = new Random();
            var offSet = random.Next(0, 201);
            return await GetMusicTracks(offSet, tag);
        }

        public async Task<byte[]?> GetImageDate(string url)
        {
            HttpClient httpClient = new HttpClient();
            var imageData = await httpClient.GetByteArrayAsync(url);

            return imageData;
        }
    }
}
