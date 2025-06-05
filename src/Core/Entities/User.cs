using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Otter.Core.Entities;

public class User : BaseEntity
{
    [Required]
    [Column("first_name", TypeName = "varchar(100)")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Column("last_name", TypeName = "varchar(100)")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Column("gender", TypeName = "varchar(1)0")]
    public string gender { get; set; } = string.Empty;

    [Column("birthday", TypeName = "date")]
    public DateOnly? Birthday { get; set; }

    [Required]
    [Column("status", TypeName = "varchar(3)")]
    public string Status { get; set; } = "ACT";

    [Required]
    [Column("email", TypeName = "varchar(100)")]
    public string Email { get; set; } = string.Empty;

    [Column("phone_number", TypeName = "varchar(15)")]
    public string? PhoneNumber { get; set; }

    [Required]
    [Column("password", TypeName = "varchar(256)")]
    public string PasswordHash { get; set; } = string.Empty;
    
    public void SetPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        PasswordHash = Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return PasswordHash == Convert.ToBase64String(hash);
    }
}