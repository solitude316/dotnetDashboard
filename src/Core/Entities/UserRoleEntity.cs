using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Otter.Core.Entities;

public class UserRoleEntity : BaseEntity
{
    [Required]
    [Column("user_id", TypeName = "uuid")]
    public Guid UserId { get; set; }

    [Required]
    [Column("role_id", TypeName = "uuid")]
    public Guid RoleId { get; set; }

    [Required]
    [Column("status", TypeName = "smallint")]
    public Int16 Status { get; set; } = 1; // 1: Active, 0: Inactive

    [Column("description", TypeName = "text")]
    public string Description { get; set; } = string.Empty;

    public static string getStatusText(Int16 status)
    {
        return status switch
        {
            1 => "Active",
            0 => "Inactive",
            _ => "Unknown"
        };
    }
}