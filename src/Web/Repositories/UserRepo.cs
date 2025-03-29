using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dashboard.Web.Entities;
using Dashboard.Web.Context;
using Dashboard.Web.Interfaces.Repositories;


namespace Dashboard.Web.Repositories;
public class UserRepo : IUserRepo
{
    private readonly UserDbContext _context;

    public UserRepo(UserDbContext context)
    {
        _context = context;
    }

    public async Task<User?> Get(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync<User>(u => u.id == id)!;
    }

    public async Task<User> Create(User user)
    {
        user.id = Guid.NewGuid();
        user.status = "A";

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }
}