using Microsoft.EntityFrameworkCore;
using RbacWebApp.Models;

namespace RbacWebApp.Services;

public class MenuService
{
    private readonly AppDbContext _db;

    public MenuService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<MenuItem>> GetMenuForUserAsync(string userName)
    {
        var menuItems = await _db.RoleMenuItems
            .Where(rm => rm.Role.UserRoles.Any(ur => ur.UserName == userName))
            .Select(rm => rm.MenuItem)
            .Distinct()
            .ToListAsync();
        return menuItems;
    }
}
