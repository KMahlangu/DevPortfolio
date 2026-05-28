using DevPortfolio.Data;
using DevPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace DevPortfolio.Controllers;

public class PortfolioController : Controller
{
    // Helper method to get skills - avoids repeating code
    private readonly ApplicationDbContext _context;

    public PortfolioController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /portfolio/skills
    public IActionResult Skills()
    {
        var skills = _context.Skills.ToList();

        // Group skills by category (for the challenge!)
        var groupedSkills = skills
            .GroupBy(s => s.Category ?? "Uncategorized")
            .OrderBy(g => g.Key);

        return View(groupedSkills); // Pass grouped skills to view
    }

    // GET: /portfolio/skillsdetails/5
    public IActionResult SkillsDetails(int id)
    {
        var skills = _context.Skills.ToList();

        // Find the skill with matching ID
        var skill = skills.FirstOrDefault(s => s.Id == id);

        if (skill == null)  // ✅ Fixed: check if skill is null, not the list
        {
            return NotFound(); // Return 404 if not found
        }

        return View(skill); // Pass the single skill to view
    }

    // GET: /portfolio/projects
    public IActionResult Projects()
    {
        var projects = _context.Projects.ToList();

        return View(projects);
    }

    // GET: /portfolio/certificates
    public IActionResult Certificates()
    {
        // You can add hardcoded certificates here later
        var certificates = _context.Certificates.ToList();
        
        return View();
    }
}