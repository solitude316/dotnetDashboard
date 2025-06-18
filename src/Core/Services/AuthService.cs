using Otter.Core.Entities;
using Otter.Core.Repositories;
using Otter.Core.Interfaces.Repositories;
using Otter.Core.Interfaces.Services;

namespace Otter.Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<User> LoginAsync(string account, string password)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Email = account
        };

        // user.SetPassword(password);


        await _userRepository.CreateAsync(user);
        return user;
    }

    public Task<User> LogoutAsync(User user)
    {
        throw new NotImplementedException("Method not implemented.");
    }
}