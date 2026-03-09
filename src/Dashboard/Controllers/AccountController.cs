using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Dto;
using Dashboard.Validators;
using Dashboard.Services;


namespace Dashboard.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController( IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistDto registInfo)
    {
        var validator = new UserRegistValidator();
        var validateResult = validator.Validate(registInfo);

        if (!validateResult.IsValid)
        {
            var errors = validator.ExtractErrors(validateResult);
            return BadRequest(errors);
        }

        try {
            var addedResult = await _accountService.RegisterAsync(registInfo);

            return Ok("");
        } catch (Exception ex)
        {
            var message = ex.Message;
            // Log the exception and return an appropriate error response
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}