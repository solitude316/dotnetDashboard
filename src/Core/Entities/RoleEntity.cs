using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Otter.Core.Entities;

public class RoleEntity : BaseEntity
{
    [Required]
    [Column("title", TypeName = "varchar(300)")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("statis", TypeName = "varchar(300)")]
    public Int16 Status { get; set; } = 1; // 1: Active, 0: Inactive

    [Column("description", TypeName = "text")]
    public string Description { get; set; } = string.Empty;
    
    public static string GetStatusText(Int16 status)
    {
        return status switch
        {
            1 => "Active",
            0 => "Inactive",
            _ => "Unknown"
        };
    }
}

