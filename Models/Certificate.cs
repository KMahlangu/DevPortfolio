using System.ComponentModel.DataAnnotations;

namespace DevPortfolio.Models;

public class Certificate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter certified owner's name.")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = " Please enter the name of qualification certifcate.")]
    public string IssuingOrganization { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string CredentialId { get; set; }
    public string CredentialUrl { get; set; }
    public string ImageUrl { get; set; } // For certificate image/logo
    public string Description { get; set; }


}

