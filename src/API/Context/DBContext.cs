using Microsoft.EntityFrameworkCore;
using Otter.Core.Entities;

namespace Otter.API.Context;
public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        base.OnModelCreating(modelBuilder);
    }
}