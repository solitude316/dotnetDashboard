using Microsoft.EntityFrameworkCore;
using Otter.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Otter.Core.Data;

public class PGSQLContext : DbContext
{
    public PGSQLContext(DbContextOptions<PGSQLContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(dob =>
        {
            dob.ToTable("users");
            dob.HasKey(u => u.Id);
            dob.Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedOnAdd();
            dob.Property(u => u.CreateOn)
                .HasColumnName("create_on")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            dob.Property(u => u.UpdateOn)
                .HasColumnName("update_on")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            dob.Property(u => u.UpdateOn)
                .HasColumnName("update_on")
                .HasColumnType("timestamptz")
                .IsRequired(false);
            dob.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(256)
                .IsRequired();
            dob.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();
        });            

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserEntity> Users { get; set; } = default!;
}