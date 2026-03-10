using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevPortfolio.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize] //This makes the whole controller require login
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}