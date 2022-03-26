using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;

public class UserInfoContent
{
    [JsonProperty("account_info")] public AccountInfo AccountInfo { get; set; }
    [JsonProperty("recent_score")] public List<ArcSongdata> RecentScore { get; set; }
}
