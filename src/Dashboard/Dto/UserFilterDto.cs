using Dashboard.Enums;

namespace Dashboard.Dto;

public class UserFilterDto
{
    public Guid? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public GenderEnum? Gender { get; set; }
}