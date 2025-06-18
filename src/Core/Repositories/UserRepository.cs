using Otter.Core.Interfaces.Repositories;
using Otter.Core.Entities;
using Otter.Core.Data;

namespace Otter.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PGSQLContext _context;

    public UserRepository(PGSQLContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException("Method not implemented.");
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException("Method not implemented.");
    }

    public Task<User?> GetByCredentialsAsync(string email, string password)
    {
        throw new NotImplementedException("Method not implemented.");
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        int result = _context.SaveChanges();
        return user;
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException("Method not implemented.");
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException("Method not implemented.");
    }
}