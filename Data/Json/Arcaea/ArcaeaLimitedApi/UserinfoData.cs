using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaLimitedApi;

[Serializable]
public class UserinfoData
{
    [JsonProperty("data")] public UserinfoDataItem Data { get; set; }
}
