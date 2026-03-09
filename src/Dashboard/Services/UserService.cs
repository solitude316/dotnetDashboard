using Dashboard.Entities;
using Dashboard.Repositories;

namespace Dashboard.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAll();
    }


    public async Task<int> AddUser(User user)
    {
        return await _userRepository.AddAsync(user);
    }
}