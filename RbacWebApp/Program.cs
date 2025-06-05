using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RbacWebApp.Models;
using RbacWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("RBAC"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ITOnly", policy =>
        policy.RequireClaim("Department", "IT"));
});

builder.Services.AddScoped<IUserRoleMappingService, AdUserRoleMappingService>();

var app = builder.Build();

void Seed(AppDbContext db)
{
    if (!db.Roles.Any())
    {
        db.Roles.AddRange(
            new Role { Id = 1, Name = "Admin", AdGroup = "IT_Admins" },
            new Role { Id = 2, Name = "Manager", AdGroup = "Managers" },
            new Role { Id = 3, Name = "User", AdGroup = "Users" }
        );

        db.MenuItems.AddRange(
            new MenuItem { Id = 1, Title = "Home", Url = "/" },
            new MenuItem { Id = 2, Title = "Admin", Url = "/Admin" }
        );

        db.RoleMenuItems.AddRange(
            new RoleMenuItem { RoleId = 1, MenuItemId = 1 },
            new RoleMenuItem { RoleId = 1, MenuItemId = 2 },
            new RoleMenuItem { RoleId = 2, MenuItemId = 1 },
            new RoleMenuItem { RoleId = 3, MenuItemId = 1 }
        );

        db.SaveChanges();
    }
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Seed(db);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
