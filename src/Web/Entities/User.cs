
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Web.Entities;

[Index(nameof(gender), Name = "Index_user_gender")]
public class User : BaseEntity
{
    [Column("first_name", TypeName = "varchar(100)")]
    public string firstName { get; set; } = "";

    [Column("last_name")]
    public string lastName { get; set; } = "";

    [Column("gender", TypeName = "varchar(1)")]
    public string gender { get; set; } = "";

    [Column("birthday", TypeName = "date")]
    public DateTime birthday { get; set; }

    [Column("status", TypeName = "varchar(3)")]
    public string status { get; set; } = "";

    [Column("email", TypeName = "varchar(100)")]
    [EmailAddress]
    public string email { get; set; } = "";

    [Column("phone", TypeName = "varchar(20)")]
    public string phone { get; set; } = "";

    private string _password = "";

    [Column("password", TypeName = "varchar(128)")]
    public string password { 
        get => _password; 
        set {
            if (value.Length >= 8)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    _password = builder.ToString();
                }
            }
        }
    }
}