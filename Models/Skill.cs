using System.ComponentModel.DataAnnotations;


namespace DevPortfolio.Models;

public class Skill
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Skill name is required.")]
    public string Name { get; set; } = string.Empty;
    [Range(1, 100, ErrorMessage = "Level must be between 1 and 100.")]
    public int Level { get; set; }
    public string? Category { get; set; }
}