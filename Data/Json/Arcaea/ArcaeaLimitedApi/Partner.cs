using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaLimitedApi;

[Serializable]
public class Partner
{
    [JsonProperty("partner_id")] public int PartnerId { get; set; }
    [JsonProperty("is_awakened")] public bool IsAwakened { get; set; }
}
