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
    private readonly IWebHostEnvironment _env;
    public HomeController(ApplicationDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
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

    // POST: Handle form submissions
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Create a new ContactMEssage from the form data
                var contactMessage = new ContactMessages
                {
                    Name = model.Name,
                    Email = model.Email,
                    Subject = model.Subject,
                    Message = model.Message,
                    DateSent = DateTime.Now,
                    IsRead = false,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    UserAgent = Request.Headers["User-Agent"].ToString()
                };


                // Save to Database
                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                // for now, just show success message
                //Later we'll add actual email sending
                TempData["Success"] = "Thank you for your message! I'll get back to you soon";
                return RedirectToAction(nameof(Contact));
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error saving contact message: {ex.Message}");

                ModelState.AddModelError("", "An error occured while sending your message. Please try again.");
            }
        }


        // if we got here, something went wrong - redisplay from 
        return View(model);
    }

    // Optional: Email notification method

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
