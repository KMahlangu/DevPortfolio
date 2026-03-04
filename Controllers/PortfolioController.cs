using DevPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DevPortfolio.Controllers;

public class PortfolioController : Controller
{
    // Helper method to get skills - avoids repeating code
    private List<Skill> GetSkills()
    {
        return new List<Skill>
        {
            new Skill { Id = 1, Name = "C#", Level = 80, Category = "Backend" },
            new Skill { Id = 2, Name = "ASP.NET Core", Level = 75, Category = "Backend" },
            new Skill { Id = 3, Name = "JavaScript", Level = 70, Category = "Frontend" },
            new Skill { Id = 4, Name = "HTML/CSS", Level = 85, Category = "Frontend" },
            new Skill { Id = 5, Name = "SQL", Level = 65, Category = "Database" }
        };
    }

    // GET: /portfolio/skills
    public IActionResult Skills()
    {
        var skills = GetSkills();

        // Group skills by category (for the challenge!)
        var groupedSkills = skills
            .GroupBy(s => s.Category ?? "Uncategorized")
            .OrderBy(g => g.Key);

        return View(groupedSkills); // Pass grouped skills to view
    }

    // GET: /portfolio/skillsdetails/5
    public IActionResult SkillsDetails(int id)
    {
        var skills = GetSkills();

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
        // You can add hardcoded projects here later
        var projects = new List<Project>
        {
            new Project
            {
                Id = 1,
                Title = "Portfolio Website",
                Description = "My personal portfolio built with ASP.NET Core MVC",
                GitHubUrl = "https://github.com/yourusername/portfolio",
                Technologies = "C#, ASP.NET Core, Bootstrap"
            },

            new Project {
            Id = 2,
            Title = "Task Manager App",
            Description = "A simple task management application",
            LiveUrl = "https://example.com",
            Technologies = "JavaScript, Node.js, MongoDB"
            },
        };

        return View(projects);
    }

    // GET: /portfolio/certificates
    public IActionResult Certificates()
    {
        // You can add hardcoded certificates here later
        return View();
    }
}