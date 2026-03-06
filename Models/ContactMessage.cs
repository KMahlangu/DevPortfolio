using System.Net;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace DevPortfolio.Models;

public class ContactMessages
{
    public int Id { get; set; } 

    [Display(Name = "First Name")]
    [Required(ErrorMessage = "Please enter your name")]
    public string Name { get; set; } = string.Empty;
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Please enter your Email")]
    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Subject is required")]
    [StringLength(200, ErrorMessage = "Subject cannot exceed 200 characters")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Message is required")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 1000 characters")]
    [DataType(DataType.MultilineText)]
    public string Message { get; set; }

    // Metadata - automatically track when message was sent
    [Display(Name = "Date Sent")]
    [DataType(DataType.DateTime)]
    public DateTime DateSent { get; set; }

    // Status tracking
    [Display(Name = "Is Read")]
    public bool IsRead { get; set; }

    [Display(Name = "Date Read")]
    [DataType(DataType.DateTime)]
    public DateTime? DateRead { get; set; }

    [Display(Name = "IP Address")]
    public string? IpAddress { get; set; }

    [Display(Name = "User Agent")]
    public string? UserAgent { get; set; }

}