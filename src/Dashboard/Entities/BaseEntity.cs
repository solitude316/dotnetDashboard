using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Entities;

public class BaseEntity
{
    private Guid _id;
    [Key]
    [Column("id")]
    public Guid Id {
        get => _id; 
        set => _id = value == default ? Guid.NewGuid() : value; 
    }

    private DateTime _createdAt;

    [Column("created_at")]
    public DateTime CreatedAt { 
        get => _createdAt;
        set => _createdAt = value == default ? DateTime.UtcNow : value;
    }

    private DateTime _updatedAt;

    [Column("updated_at")]
    public DateTime UpdatedAt { 
        get => _updatedAt;
        set => _updatedAt = DateTime.UtcNow; 
    }
}