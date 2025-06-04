using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RbacWebApp.Models;
using RbacWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("RBAC"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ITOnly", policy =>
        policy.RequireClaim("Department", "IT"));
});

builder.Services.AddScoped<IUserRoleMappingService, AdUserRoleMappingService>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();
