using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

namespace Otter.Core.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column("id", TypeName = "uuid")]
    public Guid Id { get; set; }

    private DateTime _create_date = DateTime.UtcNow;

    [Column("create_date", TypeName = "timestamptz")]
    public DateTime CreateOn
    {
        get => _create_date;
        set => _create_date = value.ToUniversalTime();
    }

    private DateTime? _update_date;
    [Column("update_date", TypeName = "timestamptz")]
    public DateTime? UpdateOn
    {
        get => _update_date;
        set => _update_date = value?.ToUniversalTime();
    }
}