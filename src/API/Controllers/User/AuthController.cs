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
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    private ILogger<AuthController> _logger;
    public AuthController(ILogger<AuthController> logger, IUserRepository userRepository)
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
    public ActionResult<string> Login()
    {
        // Placeholder for authentication logic
        return Ok("Token");
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <returns>A string indicating the registration status.</returns>
    [HttpPost("regist")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid_request");
        }

        RegisterValidator validator = new RegisterValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return BadRequest(ModelState);
        }

        try
        {
            var user = new UserEntity
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _userRepository.CreateAsync(user);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while registering the user: {ErrorMessage}", exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }

        return Ok("User registered successfully");
    }

    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid_request");
        }

        RegisterValidator validator = new RegisterValidator();
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