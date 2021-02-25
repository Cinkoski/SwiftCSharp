using SwiftCSharp.Models;
using Utf8Json;
using Utf8Json.Resolvers;

namespace SwiftCSharp
{
    public class ProgramGuide
    {
        private const string _categoryGuideUrl = "http://api.deltamediaplayer.com/rest/playListLiveCat";

        public PlaylistModel GetAllChannels()
        {
            var result = Helper.WebClient.GetAsync(_categoryGuideUrl).Result;
            var content = result.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<PlaylistModel>(content, StandardResolver.CamelCase);
        }

        public void GetSpecificChannel()
        {

        }
    }
}