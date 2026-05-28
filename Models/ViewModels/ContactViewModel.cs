using System.ComponentModel.DataAnnotations;

namespace DevPortfolio.Models.ViewModels;

public class ContactViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    [Display(Name = "Full Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Subject is required")]
    [StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Message is required")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 1000 characters")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Your Message")]
    public string Message { get; set; } = string.Empty;
}