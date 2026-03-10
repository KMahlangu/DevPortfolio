using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevPortfolio.Data;
using DevPortfolio.Models;

namespace DevPortfolio.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ProjectsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ProjectsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    // GET: Admin/Projects
    public async Task<IActionResult> Index()
    {
        var projects = await _context.Projects.ToListAsync();
        return View(projects);
    }

    // GET: Admin/Projects/Create
    public IActionResult Create()  // Removed unnecessary async
    {
        return View();
    }

    // POST Admin/Projects/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Description,GitHubUrl,LiveUrl,Technologies")] Project project, IFormFile? imageFile)
    {
        if (ModelState.IsValid)
        {
            // Handle image upload
            if (imageFile != null && imageFile.Length > 0)
            {
                // Create uploads folder if it doesn't exist
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "projects");
                Directory.CreateDirectory(uploadsFolder);

                // ✅ FIXED: Added parentheses to ToString()
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Save path to database
                project.ImageUrl = "/uploads/projects/" + uniqueFileName;
            }

            // Add project to database
            _context.Add(project);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Project created successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(project);
    }

    // GET: Admin/Projects/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // ✅ FIXED: Added id parameter
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    // POST: Admin/Projects/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,GitHubUrl,LiveUrl,Technologies,ImageUrl")] Project project, IFormFile? imageFile)
    {
        if (id != project.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Handle new image upload
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(project.ImageUrl))
                    {
                        string oldFilePath = Path.Combine(_hostEnvironment.WebRootPath,
                            project.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Save new image
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "projects");
                    Directory.CreateDirectory(uploadsFolder);

                    // ✅ FIXED: Added parentheses to ToString()
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // ✅ FIXED: Consistent folder name (projects with 's')
                    project.ImageUrl = "/uploads/projects/" + uniqueFileName;
                }

                _context.Update(project);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Project updated successfully";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(project.Id))
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
        return View(project);
    }

    // GET: Admin/Projects/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    // POST: Admin/Projects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // ✅ FIXED: Added id parameter
        var project = await _context.Projects.FindAsync(id);

        if (project != null)
        {
            // Delete image file if exists
            if (!string.IsNullOrEmpty(project.ImageUrl))
            {
                string filePath = Path.Combine(_hostEnvironment.WebRootPath,
                    project.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Project deleted successfully";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
}