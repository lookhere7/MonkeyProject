# ASP.NET Core RBAC with Active Directory

This repository contains instructions and a basic outline for implementing a
role-based access control (RBAC) back‑office that integrates with Active
Directory (AD) in ASP.NET Core.

## 1. Integrating with AD

Use Windows authentication if the application runs inside an intranet. If that
is not possible, integrate using LDAP, SAML, or OIDC so users can log in with
AD accounts.

## 2. Roles and Permissions

Define roles such as `Admin`, `Manager`, and `User`. Store the role and
permission mapping in a database (e.g., tables `Users`, `Roles`, `UserRoles`,
`Permissions`, `RolePermissions`, `MenuItems`, and `RoleMenuItems`).

## 3. Mapping AD Groups

Map AD groups to system roles. For example, the AD group `IT_Admins` can map to
the `Admin` role.

```csharp
// register a service that reads AD groups and updates UserRoles
dependencyInjectionServices.AddScoped<IUserRoleMappingService, AdUserRoleMappingService>();
```

## 4. Authorization Checks

Use the `[Authorize(Roles = "Admin")]` attribute to limit access to controllers
or actions. For more complex scenarios use policies or claims-based
authorization.

```csharp
services.AddAuthorization(options =>
{
    options.AddPolicy("ITOnly", policy =>
        policy.RequireClaim("Department", "IT"));
});
```

## 5. Dynamic Menu Generation

After login, load the menu items allowed for the user from the database and
build the side menu dynamically in Razor views.

---

This file summarizes the guidance for implementing AD integrated RBAC in an
ASP.NET Core application.
