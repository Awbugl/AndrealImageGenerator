using Newtonsoft.Json;

#pragma warning disable CS8618

namespace ImageGenerator.Json.ArcaeaLimited;

[Serializable]
public class Partner
{
    [JsonProperty("partner_id")] public int PartnerID { get; set; }
    [JsonProperty("is_awakened")] public bool IsAwakened { get; set; }
}
