using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevPortfolio.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "Project description is required.")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Please enter a valid URL.")]
    public string? GitHubUrl { get; set; } // e.g., GitHub repo or live demo
}