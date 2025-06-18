using Otter.Core.Entities;

namespace Otter.Core.Interfaces.Services;

public interface IAuthService
{
    Task<User> LoginAsync(string account, string password);

    Task<User> LogoutAsync(User user);
}