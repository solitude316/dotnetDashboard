using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Otter.API.Models.User;

public class RegisterRequest
{
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;

    
    [MinLength(1)]
    [MaxLength(1)]
    [JsonPropertyName("gender")]
    public string Gender { get; set; } = "";

    [JsonPropertyName("birthday")]
    public DateOnly? Birthday { get; set; } = null;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}   