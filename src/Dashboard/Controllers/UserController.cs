using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Repositories;
using Dashboard.Entities;

namespace Dashboard.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        var addedUser = await _userRepository.Add(user);
        return Ok(addedUser);
    }
}