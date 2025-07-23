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
using Otter.Core.Services;
using Otter.Core.Exceptions;

namespace Otter.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<RegistController> _logger;

    public RegistController(ILogger<RegistController> logger, IAuthService authSercice)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _authService = authSercice ?? throw new ArgumentNullException(nameof(authSercice));
    }
    
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <returns>A string indicating the registration status.</returns>
    [HttpPost("do-regist")]
    public async Task<IActionResult> RegistAsync([FromBody] RegisterRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid_request");
        }

        RegistValidator validator = new RegistValidator();
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
            var result = await _authService.RegistAsync(request.Email, request.Password);
        }
        catch (UserIsExitedException ex)
        {
            _logger.LogWarning(ex, "User registration failed: {ErrorMessage}", ex.Message);
            return BadRequest(new
            {
                Code = "00001",
                Message = ex.Message
            });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while registering the user: {ErrorMessage}", exception.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }

        return Ok(new
        {
            Code = "00000",
            Message = "Registration successful"
        });
    }
}