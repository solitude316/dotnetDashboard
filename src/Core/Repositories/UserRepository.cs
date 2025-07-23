using Otter.Core.Entities;
using Otter.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Otter.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PGSQLContext _context;

    public UserRepository(PGSQLContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<UserEntity?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException("Method not implemented.");
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.CompareTo(email) == 0);

        return user;
    }

    public Task<UserEntity?> GetByCredentialsAsync(string email, string password)
    {
        throw new NotImplementedException("Method not implemented.");
    }

    public async Task<UserEntity> CreateAsync(UserEntity user)
    {
        await _context.Users.AddAsync(user);
        int result = await _context.SaveChangesAsync();
        return user;
    }

    public Task UpdateAsync(UserEntity user)
    {
        throw new NotImplementedException("Method not implemented.");
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException("Method not implemented.");
    }
}