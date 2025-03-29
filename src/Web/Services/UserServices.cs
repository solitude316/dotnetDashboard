using Dashboard.Web.Repositories;
using Dashboard.Web.Entities;
using Dashboard.Web.Interfaces.Services;
using Dashboard.Web.Interfaces.Repositories;
namespace Dashboard.Web.Services;

public class UserServices : IUserService
{
    private readonly IUserRepo _userRepo;

    public UserServices(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<User> Get(Guid id)
    {
        return await _userRepo.Get(id);
    }

    public async Task<User> Create(CreateUserRequestDto userDto)
    {
        var newUser = new User() {
            id = Guid.NewGuid(),
            firstName = userDto.firstName,
            lastName = userDto.lastName,
            gender = userDto.gender,
            birthday = userDto.birthday.ToDateTime(TimeOnly.MinValue),
            email = userDto.email,
            phone = userDto.phone,
            password = userDto.password
        };

        return await _userRepo.Create(newUser);
    }
}
