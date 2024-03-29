using Newtonsoft.Json;

#pragma warning disable CS8618

namespace AndrealImageGenerator.Beans.Json;

public class SongListContent
{
    [JsonProperty("songs")]
    public List<SongsItem> Songs { get; set; }
}

public class SongsItem
{
    [JsonProperty("song_id")]
    public string SongID { get; set; }

    [JsonProperty("difficulties")]
    public ArcaeaSong Difficulties { get; set; }

    [JsonProperty("alias")]
    public List<string> Alias { get; set; }
}
