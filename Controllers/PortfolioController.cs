using DevPortfolio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DevPortfolio.Controllers;

public class PortfolioController : Controller
{
    // GET: /portfolio/skills
    public IActionResult Skills()
    {
        // In a real application, you would typically retrieve this data from a database
        var skills = new List<Skill>
        {
            new Skill { Id = 1, Name = "C#", Level = 80, Category = "Backend" },
            new Skill { Id = 2, Name = "ASP.NET Core", Level = 75, Category = "Backend" },
            new Skill { Id = 3, Name = "JavaScript", Level = 70, Category = "Frontend" },
            new Skill { Id = 4, Name = "HTML/CSS", Level = 85, Category = "Frontend" },
            new Skill { Id = 5, Name = "SQL", Level = 65, Category = "Database"}
        };

        return View(skills); // Pass the list to view.
    }

    public IActionResult SkillsDetails(int id)
    {
        // In a real application, you would typically retrieve this data from a database
        var Skills = new List<Skill>
        {
            new Skill { Id = 1, Name = "C#", Level = 80, Category = "Backend" },
            new Skill { Id = 2, Name = "ASP.NET Core", Level = 75, Category = "Backend" },
            new Skill { Id = 3, Name = "JavaScript", Level = 70, Category = "Frontend" },
            new Skill { Id = 4, Name = "HTML/CSS", Level = 85, Category = "Frontend" },
            new Skill { Id = 5, Name = "SQL", Level = 65, Category = "Database"}
        };

        // Find the Skill with matching ID
        var skill = Skills.FirstOrDefault(s => s.Id == id);

        if (Skills == null)
        {
            return NotFound(); // Return 404 if not found
        }
        
        // Pass the Skill to the view
        return View(skill);
    }

    // GET: /portfolio/projects
    public IActionResult Projects()
    {

        return View();
    }

    public IActionResult Certificates()
    {
        return View();
    }


}