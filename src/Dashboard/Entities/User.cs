using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Entities;


[Table("users")]
public class User : BaseEntity
{
    [Required]
    [Column("first_name")]
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [Required]
    [Column("last_name")]
    [MaxLength(100)]
    public string? LastName { get; set; }

    [Required]
    [Column("gender")]
    public Dashboard.Enums.GenderEnum Gender { get; set; }

    [Required]
    [Column("birthday")]
    public DateTime Birthday { get; set; }
    
}