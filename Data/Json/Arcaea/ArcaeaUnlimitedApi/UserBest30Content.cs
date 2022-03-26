using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;

public class UserBest30Content
{
    [JsonProperty("best30_avg")] public double Best30Avg { get; set; }
    [JsonProperty("recent10_avg")] public double Recent10Avg { get; set; }
    [JsonProperty("account_info")] public AccountInfo AccountInfo { get; set; }
    [JsonProperty("best30_list")] public List<ArcSongdata> Best30List { get; set; }
}
