using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaLimitedApi;

[Serializable]
public class ScoreinfoData
{
    [JsonProperty("data")] public RecordDataItem Data { get; set; }
}
