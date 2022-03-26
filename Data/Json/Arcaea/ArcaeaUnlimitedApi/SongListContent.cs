using ImageGenerator.Data.Json.Arcaea.Songlist;
using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;

public class SongListContent
{
    [JsonProperty("songs")] public List<SongsItem> Songs { get; set; }
}
