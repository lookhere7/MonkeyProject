namespace RbacWebApp.Models;

public class UserRole
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;
    public int RoleId { get; set; }
    public Role Role { get; set; } = default!;
    public string UserName { get; set; } = string.Empty;
}
