using Microsoft.EntityFrameworkCore;
using Dashboard.Web.Entities;

namespace Dashboard.Web.Context;

public class UserDbContext: DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}