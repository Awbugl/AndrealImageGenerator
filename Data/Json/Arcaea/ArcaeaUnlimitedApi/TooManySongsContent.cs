using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;

public class TooManySongsContent
{
    [JsonProperty("songs")] public List<string> Songs { get; set; }
}
