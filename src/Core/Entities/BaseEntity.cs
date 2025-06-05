using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Otter.Core.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column("id", TypeName = "uuid")]
    public Guid Id { get; set; }

    private DateTime _create_on = DateTime.UtcNow;

    [Column("create_on", TypeName = "timestamptz")]
    public DateTime CreateOn
    {
        get => _create_on;
        set => _create_on = value.ToUniversalTime();
    }

    private DateTime? _update_on;
    [Column("update_on", TypeName = "timestamptz")]
    public DateTime? UpdateOn
    {
        get => _update_on;
        set => _update_on = value?.ToUniversalTime();
    }
}