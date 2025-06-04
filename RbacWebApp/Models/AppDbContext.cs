using Microsoft.EntityFrameworkCore;

namespace RbacWebApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<RoleMenuItem> RoleMenuItems => Set<RoleMenuItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
