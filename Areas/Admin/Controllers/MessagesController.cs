using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevPortfolio.Data;
using DevPortfolio.Models;

namespace DevPortfolio.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class MessagesController : Controller
{
    private readonly ApplicationDbContext _context;

    public MessagesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Messages
    public async Task<IActionResult> Index()
    {
        var messages = await _context.ContactMessages
            .OrderByDescending(m => m.DateSent)
            .ToListAsync();
        return View(messages);
    }

    // GET: Admin/Messages/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || id <= 0)
        {
            return NotFound();
        }

        var message = await _context.ContactMessages
            .FirstOrDefaultAsync(m => m.Id == id);

        if (message == null)
        {
            return NotFound();
        }

        // Mark as read when viewed
        if (!message.IsRead)
        {
            message.IsRead = true;
            message.DateRead = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        return View(message);
    }

    // POST: Admin/Messages/MarkAsRead/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message != null)
        {
            message.IsRead = true;
            message.DateRead = DateTime.Now;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Message marked as read.";
        }
        return RedirectToAction(nameof(Index));
    }

    // POST: Admin/Messages/MarkAsUnread/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsUnread(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message != null)
        {
            message.IsRead = false;
            message.DateRead = null;
            await _context.SaveChangesAsync();
            TempData["Success"] = "Message marked as unread.";
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Messages/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var message = await _context.ContactMessages
            .FirstOrDefaultAsync(m => m.Id == id);
        if (message == null)
        {
            return NotFound();
        }

        return View(message);
    }

    // POST: Admin/Messages/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message != null)
        {
            _context.ContactMessages.Remove(message);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Message deleted successfully!";
        }
        return RedirectToAction(nameof(Index));
    }

    // POST: Admin/Messages/DeleteAll
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAll()
    {
        var allMessages = await _context.ContactMessages.ToListAsync();
        if (allMessages.Any())
        {
            _context.ContactMessages.RemoveRange(allMessages);
            await _context.SaveChangesAsync();
            TempData["Success"] = "All messages deleted successfully!";
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Admin/Messages/Export
    public async Task<IActionResult> Export()
    {
        var messages = await _context.ContactMessages
            .OrderByDescending(m => m.DateSent)
            .ToListAsync();

        // Create CSV content
        var csv = "Id,Name,Email,Subject,Message,DateSent,IsRead,DateRead,IpAddress\n";
        foreach (var m in messages)
        {
            csv += $"{m.Id},\"{m.Name}\",\"{m.Email}\",\"{m.Subject}\",\"{m.Message.Replace("\"", "\"\"")}\",{m.DateSent:yyyy-MM-dd HH:mm:ss},{m.IsRead},{m.DateRead:yyyy-MM-dd HH:mm:ss},{m.IpAddress}\n";
        }

        var bytes = System.Text.Encoding.UTF8.GetBytes(csv);
        return File(bytes, "text/csv", $"messages-{DateTime.Now:yyyy-MM-dd}.csv");
    }

    private bool MessageExists(int id)
    {
        return _context.ContactMessages.Any(e => e.Id == id);
    }
}