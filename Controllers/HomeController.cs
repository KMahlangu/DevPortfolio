using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevPortfolio.Models;

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
