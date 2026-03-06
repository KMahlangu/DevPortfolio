using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevPortfolio.Models;
using System.Linq;
using System.Collections.Generic;
using SQLitePCL;
using DevPortfolio.Data;


namespace DevPortfolio.Controllers;

public class HomeController : Controller
{

    // Constructor - gets database context automatically
    private readonly ApplicationDbContext _context;
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
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
        var services = _context.Services.ToList();
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
