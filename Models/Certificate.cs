using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPortfolio.Models;

public class Certificate
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Certificate title is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 200 characters")]
    [Display(Name = "Certificate Title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Issuing organization is required")]
    [StringLength(100, ErrorMessage = "Organization name cannot exceed 100 characters")]
    [Display(Name = "Issuing Organization")]
    public string IssuingOrganization { get; set; } = string.Empty;

    [Required(ErrorMessage = "Issue date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Issue Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime IssueDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Expiration Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? ExpirationDate { get; set; }

    [Display(Name = "Credential ID")]
    [StringLength(100, ErrorMessage = "Credential ID cannot exceed 100 characters")]
    public string? CredentialId { get; set; }

    [Url(ErrorMessage = "Please enter a valid URL")]
    [Display(Name = "Credential URL")]
    [StringLength(500, ErrorMessage = "URL cannot exceed 500 characters")]
    public string? CredentialUrl { get; set; }

    [Display(Name = "Certificate File")]
    public string? CertificateFileUrl { get; set; }

    [Display(Name = "Skills Gained")]
    [StringLength(500, ErrorMessage = "Skills description cannot exceed 500 characters")]
    public string? SkillsGained { get; set; }

    [Display(Name = "Certificate Number")]
    public string? CertificateNumber { get; set; }

    [Display(Name = "Does Not Expire")]
    public bool DoesNotExpire { get; set; }

    // Display properties (not stored in database)
    [NotMapped]
    [Display(Name = "Upload Certificate (PDF only)")]
    public IFormFile? CertificateFile { get; set; }

    [NotMapped]
    public bool IsExpired => ExpirationDate.HasValue && ExpirationDate.Value < DateTime.Now;

    [NotMapped]
    public string DisplayStatus
    {
        get
        {
            if (IsExpired) return "Expired";
            if (ExpirationDate.HasValue) return "Valid until " + ExpirationDate.Value.ToString("MMM yyyy");
            if (DoesNotExpire) return "No Expiration";
            return "Valid";
        }
    }

    [NotMapped]
    public string StatusClass
    {
        get
        {
            if (IsExpired) return "danger";
            if (ExpirationDate.HasValue) return "success";
            return "info";
        }
    }
}