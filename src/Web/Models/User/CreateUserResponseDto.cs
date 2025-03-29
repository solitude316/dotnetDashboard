using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dashboard.Web.Models.User;

public class CreateUserResponseDto
{
    [Required]
    [JsonPropertyName("status_code")]
    public string statusCode { get; set; } = "";

    [JsonPropertyName("status_message")]
    public string statusMessage { get; set; } = "";
}