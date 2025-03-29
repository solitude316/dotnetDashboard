using Dashboard.Web.Entities;

namespace Dashboard.Web.Interfaces.Repositories;

public interface IUserRepo
{
    Task<User?> Get(Guid id);
    Task<User> Create(User user);
}