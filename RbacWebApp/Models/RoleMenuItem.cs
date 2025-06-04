namespace RbacWebApp.Models;

public class RoleMenuItem
{
    public int RoleId { get; set; }
    public Role Role { get; set; } = default!;
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
}
