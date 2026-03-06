using System.Net;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace DevPortfolio.Models;

public class ContactMessages
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; }
    public string Subject { get; set; }
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