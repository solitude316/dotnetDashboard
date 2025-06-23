using Otter.Core.Entities;
using Otter.Core.Repositories;
using Otter.Core.Services;

namespace Otter.Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<UserEntity> LoginAsync(string account, string password)
    {
        UserEntity user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = account
        };

        // user.SetPassword(password);


        await _userRepository.CreateAsync(user);
        return user;
    }

    public Task<UserEntity> LogoutAsync(UserEntity user)
    {
        throw new NotImplementedException("Method not implemented.");
    }
}