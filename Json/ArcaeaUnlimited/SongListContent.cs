using ImageGenerator.Model;
using Newtonsoft.Json;

#pragma warning disable CS8618

namespace ImageGenerator.Json.ArcaeaUnlimited;

public class SongListContent
{
    [JsonProperty("songs")] public List<SongsItem> Songs { get; set; }
}

public class SongsItem
{
    [JsonProperty("song_id")] public string SongID { get; set; }
    [JsonProperty("difficulties")] public ArcaeaSong Difficulties { get; set; }
    [JsonProperty("alias")] public List<string> Alias { get; set; }
}
