using Dashboard.Enums;

namespace Dashboard.Entities;

public class Role : BaseEntity
{
    public string title { get; set; } = "";

    public RoleStatusEnum status { get; set; } = RoleStatusEnum.Undefined;

    public string code { get; set; } = "";

}