using System.ComponentModel.DataAnnotations;

namespace DevPortfolio.Models;

public class Skill
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Skill name is required.")]
    public string Name { het; set; } = string.Empty;
    public string? Category { get; set; } // e.g : Frontend, Backend, Tools
    [Range(1, 100, ErrorMessage = "proficiency must be between 1 and 100")]
    public int ProficiencyLevel { get; set; }

    public string DateAcquired { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
}