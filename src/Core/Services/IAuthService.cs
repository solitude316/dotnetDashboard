using Otter.Core.Entities;

namespace Otter.Core.Services;

public interface IAuthService
{
    Task<UserEntity> RegistAsync(string email, string password);

    Task<UserEntity> LoginAsync(string account, string password);

    Task<UserEntity> LogoutAsync(UserEntity user);
}