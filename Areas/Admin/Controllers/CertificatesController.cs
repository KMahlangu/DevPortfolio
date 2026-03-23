using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevPortfolio.Data;
using DevPortfolio.Models;

namespace DevPortfolio.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class CertificatesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public CertificatesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    // GET: Admin/Certificates
    public async Task<IActionResult> Index()
    {
        var certificates = await _context.Certificates
            .OrderByDescending(c => c.IssueDate)
            .ToListAsync();
        return View(certificates);
    }

    // GET: Admin/Certificates/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || id <= 0)
        {
            return NotFound();
        }

        var certificate = await _context.Certificates
            .FirstOrDefaultAsync(m => m.Id == id);

        if (certificate == null)
        {
            return NotFound();
        }

        return View(certificate);
    }

    // GET: Admin/Certificates/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Certificates/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,IssuingOrganization,IssueDate,ExpirationDate,CredentialId,CredentialUrl,SkillsGained")] Certificate certificate, IFormFile? pdfFile)
    {
        if (ModelState.IsValid)
        {
            // Handle PDF upload
            if (pdfFile != null && pdfFile.Length > 0)
            {
                // Validate file type
                var allowedExtensions = new[] { ".pdf", ".PDF" };
                var fileExtension = Path.GetExtension(pdfFile.FileName);

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("pdfFile", "Only PDF files are allowed.");
                    return View(certificate);
                }

                // Validate file size (max 5MB)
                if (pdfFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("pdfFile", "File size cannot exceed 5MB.");
                    return View(certificate);
                }

                // Create uploads folder if it doesn't exist
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "certificates");
                Directory.CreateDirectory(uploadsFolder);

                // Generate unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + pdfFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await pdfFile.CopyToAsync(fileStream);
                }

                // Save path to database
                certificate.CertificateFileUrl = "/uploads/certificates/" + uniqueFileName;
            }

            // Set default values
            certificate.IssueDate = certificate.IssueDate.ToUniversalTime();

            _context.Add(certificate);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Certificate created successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(certificate);
    }

    // GET: Admin/Certificates/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var certificate = await _context.Certificates.FindAsync(id);
        if (certificate == null)
        {
            return NotFound();
        }
        return View(certificate);
    }

    // POST: Admin/Certificates/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IssuingOrganization,IssueDate,ExpirationDate,CredentialId,CredentialUrl,SkillsGained,CertificateFileUrl")] Certificate certificate, IFormFile? pdfFile)
    {
        if (id != certificate.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Handle new PDF upload
                if (pdfFile != null && pdfFile.Length > 0)
                {
                    // Validate file type
                    var allowedExtensions = new[] { ".pdf", ".PDF" };
                    var fileExtension = Path.GetExtension(pdfFile.FileName);

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("pdfFile", "Only PDF files are allowed.");
                        return View(certificate);
                    }

                    // Validate file size
                    if (pdfFile.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("pdfFile", "File size cannot exceed 5MB.");
                        return View(certificate);
                    }

                    // Delete old PDF if exists
                    if (!string.IsNullOrEmpty(certificate.CertificateFileUrl))
                    {
                        string oldFilePath = Path.Combine(_hostEnvironment.WebRootPath,
                            certificate.CertificateFileUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Save new PDF
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "certificates");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + pdfFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await pdfFile.CopyToAsync(fileStream);
                    }

                    certificate.CertificateFileUrl = "/uploads/certificates/" + uniqueFileName;
                }

                _context.Update(certificate);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Certificate updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateExists(certificate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(certificate);
    }

    // GET: Admin/Certificates/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var certificate = await _context.Certificates
            .FirstOrDefaultAsync(m => m.Id == id);
        if (certificate == null)
        {
            return NotFound();
        }

        return View(certificate);
    }

    // POST: Admin/Certificates/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var certificate = await _context.Certificates.FindAsync(id);
        if (certificate != null)
        {
            // Delete PDF file if exists
            if (!string.IsNullOrEmpty(certificate.CertificateFileUrl))
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath,
                    certificate.CertificateFileUrl.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Certificate deleted successfully!";
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Certificates/Download/5
    public async Task<IActionResult> Download(int id)
    {
        var certificate = await _context.Certificates.FindAsync(id);
        if (certificate == null || string.IsNullOrEmpty(certificate.CertificateFileUrl))
        {
            return NotFound();
        }

        string filePath = Path.Combine(_hostEnvironment.WebRootPath,
            certificate.CertificateFileUrl.TrimStart('/'));

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        string fileName = Path.GetFileName(certificate.CertificateFileUrl);

        return File(fileBytes, "application/pdf", fileName);
    }

    private bool CertificateExists(int id)
    {
        return _context.Certificates.Any(e => e.Id == id);
    }
}