using Dashboard.Entities;

namespace Dashboard.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAll();
    Task<User> Add(User user);
}