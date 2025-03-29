using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Dashboard.Web.Entities;

public class BaseEntity
{
    [Key]
    [Column(("id"), TypeName = "uuid")]
    public Guid id { get; set; }

    private DateTime _createdAt = DateTime.UtcNow;

    [Column("created_at", TypeName = "timestamptz")]
    public DateTime createdAt { 
        get {
            if (_createdAt.Kind == DateTimeKind.Utc) {
                return _createdAt;
            } else {
                return DateTime.SpecifyKind(_createdAt, DateTimeKind.Utc);
            }
        }
        set {
            if (value < DateTime.UtcNow)
            {
                _createdAt = value;
            }
        }
    }
    
    private DateTime _updatedAt = DateTime.UtcNow;

    [Column("updated_at", TypeName = "timestamptz")]
    public DateTime updatedAt { 
        get {
            if (_updatedAt.Kind == DateTimeKind.Utc) {
                return _updatedAt;
            } else {
                return DateTime.SpecifyKind(_updatedAt, DateTimeKind.Utc);
            }
        }
        set {
            if (value < DateTime.UtcNow)
            {
                _updatedAt = value;
            }
        }
    }
}