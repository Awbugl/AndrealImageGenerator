using Newtonsoft.Json;

namespace ImageGenerator.Data.Json.Arcaea.ArcaeaUnlimitedApi;

public class ResponseRoot
{
    [JsonProperty("content")] public dynamic Content { get; set; }

    internal T? DeserializeContent<T>() => JsonConvert.DeserializeObject<T>(Content.ToString());
}
