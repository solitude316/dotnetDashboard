using System.Text.Json.Serialization;
using Dashboard.Web.Models.User;

public class CreateUserRequestDto : UserDto
{
    [JsonPropertyName("password")]
    public string password { get; set; } = "";
}