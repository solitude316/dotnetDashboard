using Dashboard.Entities;

namespace Dashboard.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<int> AddAsync(User user);

    Task<User?> GetById(Guid id);
}