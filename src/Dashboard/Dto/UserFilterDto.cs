using Dashboard.Enums;

namespace Dashboard.Dto;

public class UserFilterDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public GenderEnum? Gender { get; set; }
    public DateOnly? Birthday { get; set; }
}