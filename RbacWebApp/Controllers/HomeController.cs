using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RbacWebApp.Services;

namespace RbacWebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly MenuService _menuService;

    public HomeController(MenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<IActionResult> Index()
    {
        var menu = await _menuService.GetMenuForUserAsync(User.Identity?.Name ?? string.Empty);
        return View(menu);
    }
}
