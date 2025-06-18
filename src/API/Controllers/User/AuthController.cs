using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otter.API.Models.User;
using Otter.API.Validator.User;
using Otter.Core.Entities;
using Otter.Core.Interfaces.Repositories;
using Otter.Core.Repositories;

namespace Otter.API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public AuthController(IUserRepository userRepository)
    {
        // Initialize the user repository if needed
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
    [HttpPost("register")]
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
            var user = new Otter.Core.Entities.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Gender = request.Gender,
                Birthday = request.Birthday,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password
            };

            var result = await _userRepository.CreateAsync(user);
        }
        catch (Exception ex)
        {
            // Log the exception (not implemented here)
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }

        return Ok("User registered successfully");
    }
}