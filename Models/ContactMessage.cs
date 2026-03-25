using System.ComponentModel.DataAnnotations;

namespace DevPortfolio.Models;

public class ContactMessage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    [Display(Name = "Full Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(100)]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Subject is required")]
    [StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Message is required")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 1000 characters")]
    [DataType(DataType.MultilineText)]
    public string Message { get; set; } = string.Empty;

    [DataType(DataType.DateTime)]
    [Display(Name = "Date Sent")]
    public DateTime DateSent { get; set; }

    [Display(Name = "Is Read")]
    public bool IsRead { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Date Read")]
    public DateTime? DateRead { get; set; }

    [Display(Name = "IP Address")]
    public string? IpAddress { get; set; }

    [Display(Name = "User Agent")]
    public string? UserAgent { get; set; }
}