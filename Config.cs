using Newtonsoft.Json;

namespace ImageGenerator;

public class Config
{
    [JsonProperty("iconpath")] public string IconPath { get; set; } = "";
    [JsonProperty("songpath")] public string SongPath { get; set; } = "";
}
