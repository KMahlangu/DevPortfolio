using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevPortfolio.Data;
using DevPortfolio.Models;
using DevPortfolio.Models.ViewModels;
using System.Diagnostics;

namespace DevPortfolio.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: Home/Index
    public async Task<IActionResult> Index()
    {
        try
        {
            var viewModel = new HomeViewModel
            {
                Skills = await _context.Skills.ToListAsync(),
                Projects = await _context.Projects.ToListAsync(),
                Services = await _context.Services.ToListAsync()
            };
            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading home page");
            return View();
        }
    }

    // GET: Home/About
    public IActionResult About()
    {
        return View();
    }

    // GET: Home/Services
    public async Task<IActionResult> Services()
    {
        var services = await _context.Services
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();
        return View(services);
    }

    // GET: Home/Contact
    public IActionResult Contact()
    {
        return View();
    }

    // POST: Home/Contact
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Create new contact message from form data
                var contactMessage = new ContactMessage
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

                // Save to database
                _context.ContactMessages.Add(contactMessage);
                await _context.SaveChangesAsync();

                // Show success message
                TempData["SuccessMessage"] = "Thank you for your message! I'll get back to you soon.";

                return RedirectToAction(nameof(Contact));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving contact message");
                ModelState.AddModelError("", "An error occurred while sending your message. Please try again.");
            }
        }

        // If we got this far, something failed - redisplay form
        return View(model);
    }

    // GET: Home/Privacy
    public IActionResult Privacy()
    {
        return View();
    }

    // Error handling
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}