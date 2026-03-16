using Dashboard.Enums;
using Dashboard.Enums.Account;
namespace Dashboard.Dto;

public class UserFilterDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public GenderEnum? Gender { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? Email { get; set; }
    public AccountStatus account_status { get; set; }
}