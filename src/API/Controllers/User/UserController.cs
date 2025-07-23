using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otter.API.Models.User;
using Otter.API.Validator.User;
using Otter.Core.Entities;
using Otter.Core.Repositories;
using RegisterRequest = Otter.API.Models.User.RegisterRequest;
using LoginRequest = Otter.API.Models.User.LoginRequest;

namespace Otter.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    private ILogger<UserController> _logger;
    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        // Initialize the user repository if needed
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Authenticates a user and returns a token.
    /// </summary>
    /// <returns>A string representing the authentication token.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid_request");
        }

        RegistValidator validator = new RegistValidator();
        var validationResult = await validator.ValidateAsync(new Models.User.RegisterRequest
        {
            Email = request.Email,
            Password = request.Password
        });

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return BadRequest(ModelState);
        }

        return Ok("Login successful");
    }
}