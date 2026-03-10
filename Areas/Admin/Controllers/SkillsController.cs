using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevPortfolio.Data;
using DevPortfolio.Models;

namespace DevPortfolio.Areas.Admin.Controllers;  // ✅ CORRECT NAMESPACE

[Area("Admin")]      // ✅ MUST HAVE THIS
[Authorize]          // ✅ MUST HAVE THIS (requires login)
public class SkillsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SkillsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Skills
    public async Task<IActionResult> Index()
    {
        var skills = await _context.Skills.ToListAsync();
        return View(skills);
    }

    // GET: Admin/Skills/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var skill = await _context.Skills.FindAsync(id);
        if (skill == null) return NotFound();

        return View(skill);
    }

    // POST: Admin/Skills/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Skill skill)
    {
        if (id != skill.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(skill);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Skill updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(skill.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(skill);
    }

    // GET: Admin/Skills/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var skill = await _context.Skills
            .FirstOrDefaultAsync(m => m.Id == id);
        if (skill == null) return NotFound();

        return View(skill);
    }

    // POST: Admin/Skills/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill != null)
        {
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Skill deleted successfully!";
        }
        return RedirectToAction(nameof(Index));
    }

    private bool SkillExists(int id)
    {
        return _context.Skills.Any(e => e.Id == id);
    }
}