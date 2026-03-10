using System.ComponentModel.DataAnnotations.Schema;
using Dashboard.Enums.Account;

namespace Dashboard.Entities;

[Table("accounts")]
public class Account : BaseEntity
{
    public string email { get; set; }

    public string? password { get; set; }

    public AccountStatus status { get; set; }

    public DateTime last_login { get; set; }

    public AccountSource source { get; set; }
}