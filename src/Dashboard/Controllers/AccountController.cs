using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Dto;
using Dashboard.Entities;
using Dashboard.Exceptions;
using Dashboard.Services;
using Dashboard.Validators;

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
    public async Task<IActionResult> Register([FromBody] AccountDto registInfo)
    {
        var validator = new AccountValidator();
        var validateResult = validator.Validate(registInfo);

        if (!validateResult.IsValid)
        {
            var errors = validator.ExtractErrors(validateResult);
            return BadRequest (new {
                code = "00001",
                errors = errors
            });
        }

        try {
            var addedResult = await _accountService.RegisterAsync(registInfo);

            return Ok("Account Successfully Registered");
        } catch (AccountException ex) {
            return Ok(new
            {
                code = "00002",
                error = ex.Message
            });
        } catch (Exception ex) {
            var message = ex.Message;
            // Log the exception and return an appropriate error response
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AccountDto param)
    {
        var validator = new AccountValidator();
        var validateResult = validator.Validate(param);

        if (!validateResult.IsValid)
        {
            var errors = validator.ExtractErrors(validateResult);
            return BadRequest (new {
                code = "00001",
                errors = errors
            });
        }

        try
        {
            var token = await _accountService.LoginAsync(param.email, param.password);

            return Ok(new {
                token = token
            });
        } catch(AccountException ex) {
            return Ok(new {
                code = "00003",
                error = ex.Message
            });
        } catch (Exception ex) {
            var message = ex.Message;
            // Log the exception and return an appropriate error response
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}