using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.User;
using API.Validator.User;

namespace API.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
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
            return BadRequest("Invalid registration request");
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

        return Ok("User registered successfully");
    }
}

