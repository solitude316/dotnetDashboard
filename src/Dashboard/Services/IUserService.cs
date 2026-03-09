using Dashboard.Entities;

namespace Dashboard.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    
    Task<int> AddUser(User user);

    // Task<User> GetById(Guid id);
}