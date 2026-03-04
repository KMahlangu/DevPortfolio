using System.ComponentModel.DataAnnotations;

namespace DevPortfolio.Models;

public class Project
{

    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    [Url(ErrorMessage = "Please enter a valid URL")]
    public string? GitHubUrl { get; set; }
    [Url(ErrorMessage = "Please enter a valid URL")]
    public string? LiveUrl { get; set; }
    [Url(ErrorMessage = "Please enter a valid URL")]
    public string? ImageUrl { get; set; }
    public string? Technologies { get; set; } // store as comma-separated
}