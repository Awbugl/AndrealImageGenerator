using Newtonsoft.Json;

#pragma warning disable CS8618

namespace AndrealImageGenerator.Beans.Json;

public class ResponseRoot<T>
{
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("content")]
    public T Content { get; set; }
}
