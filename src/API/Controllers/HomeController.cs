using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otter.Core.Interfaces.Services;
using Otter.Core.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    IAuthService _authService;
    public HomeController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("hello")]
    public async Task<IActionResult> Get()
    {
        var user = await _authService.LoginAsync("testUser", "testPassword");

        return Ok("Hello, World!");
    }
}
