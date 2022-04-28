using Newtonsoft.Json;

#pragma warning disable CS8618

namespace ImageGenerator.Json.ArcaeaLimited;

[Serializable]
public class ScoreinfoData
{
    [JsonProperty("data")] public RecordDataItem Data { get; set; }
}
