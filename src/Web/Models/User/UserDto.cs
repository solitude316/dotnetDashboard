using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dashboard.Web.Models.User;

public class UserDto
{
    public Guid id { get; set; }

    [JsonPropertyName("first_name")]
    [Required(ErrorMessage = "必填")]
    [MaxLength(100, ErrorMessage = "最多 100 個字")]
    public string firstName { get; set; } = "";

    [JsonPropertyName("last_name")]
    [Required(ErrorMessage = "必填")]
    [MaxLength(100, ErrorMessage = "最多 100 個字")]
    public string lastName { get; set; } = "";

    [JsonPropertyName("gender")]
    [Required(ErrorMessage = "必填")]
    [MaxLength(100, ErrorMessage = "最多 100 個字")]
    public string gender { get; set; } = "";

    [JsonPropertyName("birthday")]
    public DateOnly birthday { get; set; }

    [JsonPropertyName("status")]
    public string status { get; set; } = "";

    [JsonPropertyName("email")]
    [Required(ErrorMessage = "必填")]
    [MaxLength(50, ErrorMessage = "最多 50 個字")]
    public string email { get; set; } = "";

    [JsonPropertyName("phone")]
    [Required(ErrorMessage = "必填")]
    [MaxLength(20, ErrorMessage = "最多 20 個字")]
    public string phone { get; set; } = ""; 
    
    [JsonPropertyName("created_at")]
    public DateTime createdAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime updatedAt { get; set; }
}