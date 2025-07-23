using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Otter.API.Models.User;

public class RegisterRequest
{

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}   