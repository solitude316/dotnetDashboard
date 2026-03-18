using System.ComponentModel.DataAnnotations.Schema;
using Dashboard.Enums.Account;

namespace Dashboard.Entities;

[Table("accounts")]
public class Account : BaseEntity
{
    public string email { get; set; } = "";

    public string password { get; set; } = "";

    public AccountStatusEnum status { get; set; } = AccountStatusEnum.Undefined;

    public DateTime last_login { get; set; }

    public AccountSourceEnum source { get; set; } = AccountSourceEnum.Undefined;
}