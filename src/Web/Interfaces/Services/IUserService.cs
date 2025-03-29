using Dashboard.Web.Entities;

namespace Dashboard.Web.Interfaces.Services;

public interface IUserService
{
    Task<User> Get(Guid id);
    Task<User> Create(CreateUserRequestDto user);
}