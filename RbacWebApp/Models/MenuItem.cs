namespace RbacWebApp.Models;

public class MenuItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public ICollection<RoleMenuItem> RoleMenuItems { get; set; } = new List<RoleMenuItem>();
}
