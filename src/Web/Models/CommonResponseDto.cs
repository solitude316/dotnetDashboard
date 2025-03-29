using System.Text.Json.Serialization;

namespace Dashboard.Web.Models;

public class CommonResponseDto<T>
{
    [JsonPropertyName("status_code")]
    public string statusCode { get; set; } = "";

    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    [JsonPropertyName("data")]
    public T? data { get; set; }
}