using Newtonsoft.Json;

#pragma warning disable CS8618

namespace ImageGenerator.Json.ArcaeaUnlimited;

public class TooManySongsContent
{
    [JsonProperty("songs")] public List<string> Songs { get; set; }
}
