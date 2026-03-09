
namespace Dashboard.Entities;

public class User : BaseEntity
{
    public string? first_name { get; set; }

    public string? last_name { get; set; }

    public Dashboard.Enums.GenderEnum gender { get; set; }

    public DateOnly birthday { get; set; }
    
}