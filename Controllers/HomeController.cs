using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevPortfolio.Models;
using System.Linq;
using System.Collections.Generic;

namespace DevPortfolio.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Services()
    {
        // Hardcoded services for now (since no database yet)
        var services = new List<Service>
    {
        new Service {
            Id = 1,
            Title = "Web Development",
            Description = "Custom web applications built with ASP.NET Core",
            IconClass = "bi-code-slash",
            DisplayOrder = 1
        },
        new Service {
            Id = 2,
            Title = "Database Design",
            Description = "Efficient database architecture and optimization",
            IconClass = "bi-database",
            DisplayOrder = 2
        },
        new Service {
            Id = 3,
            Title = "Cloud Solutions",
            Description = "Deploy and scale applications in the cloud",
            IconClass = "bi-cloud",
            DisplayOrder = 3
        },
        new Service {
            Id = 4,
            Title = "API Development",
            Description = "RESTful APIs for seamless integration",
            IconClass = "bi-plugin",
            DisplayOrder = 4
        }
    };

        return View(services);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult ServerTime()
    {
        var serverTime = DateTime.Now.ToString("F");
        return Content($"Current server time: {serverTime}");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
