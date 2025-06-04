using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    public HomeController()
    {

    }

    [HttpGet("hello")]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }
}
