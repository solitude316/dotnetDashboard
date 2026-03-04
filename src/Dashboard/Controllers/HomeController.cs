using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers;

[Route("[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    
    public HomeController()
    {
        
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return Ok("Hello World");
    }
}