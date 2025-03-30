using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IWebHostEnvironment _environment;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
    }

    public IActionResult Index()
    {
        var indexPath = _environment.WebRootFileProvider.GetFileInfo("index.html");

        if (!indexPath.Exists)
        {
            return NotFound("index.html not found in wwwroot folder.");
        }
        var content = 

        ViewBag.HtmlContent = System.IO.File.ReadAllText(indexPath.PhysicalPath!);

        return PartialView();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
