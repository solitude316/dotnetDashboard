using Microsoft.Extensions.Logging;
using Otter.Core.Entities;
using Otter.Core.Repositories;
using Otter.Core.Services;
using Otter.Core.Exceptions;

namespace Otter.Core.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IUserRepository _userRepository;
    public AuthService(ILogger<AuthService> logger, IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<UserEntity> RegistAsync(string email, string password)
    {
        // Check if the user already exists
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            _logger.LogWarning("User with email {Email} already exists.", email);
            throw new UserIsExitedException("User already exists.");
        }

        var newUser = new UserEntity
        {
            Email = email,
            Password = password,
        };

        _logger.LogInformation($"Creating new user  {email.ToString()}.");

        var result = await _userRepository.CreateAsync(newUser);

        return newUser;
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