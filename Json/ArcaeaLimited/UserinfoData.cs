using Newtonsoft.Json;

#pragma warning disable CS8618

namespace ImageGenerator.Json.ArcaeaLimited;

[Serializable]
public class UserinfoData
{
    [JsonProperty("data")] public UserinfoDataItem Data { get; set; }
}
