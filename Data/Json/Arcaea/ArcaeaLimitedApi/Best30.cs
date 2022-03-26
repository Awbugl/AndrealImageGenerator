using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaLimitedApi;

[Serializable]
public class Best30
{
    [JsonProperty("data")] public List<RecordDataItem> Data { get; set; }
}
