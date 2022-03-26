using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;

public class UserBestContent
{
    [JsonProperty("account_info")] public AccountInfo AccountInfo { get; set; }
    [JsonProperty("record")] public ArcSongdata Record { get; set; }
}
