using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RbacWebApp.Models;

namespace RbacWebApp.Services;

public class AdUserRoleMappingService : IUserRoleMappingService
{
    private readonly AppDbContext _db;

    public AdUserRoleMappingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task MapRolesAsync(ClaimsPrincipal user)
    {
        var groups = user.FindAll(ClaimTypes.GroupSid).Select(c => c.Value);

        foreach (var g in groups)
        {
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.AdGroup == g);
            if (role != null)
            {
                if (!await _db.UserRoles.AnyAsync(ur => ur.UserName == user.Identity!.Name && ur.RoleId == role.Id))
                {
                    _db.UserRoles.Add(new UserRole
                    {
                        UserName = user.Identity!.Name ?? string.Empty,
                        RoleId = role.Id
                    });
                }
            }
        }
        await _db.SaveChangesAsync();
    }
}
