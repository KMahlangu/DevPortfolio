using System.ComponentModel.DataAnnotations;

namespace DevPortfolio.Models;

public class Service
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Service title is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Icon Class")]
    public string? IconClass { get; set; } // For Bootstrap Icons (e.g., "bi-code-slash")

    [Display(Name = "Display Order")]
    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;
}