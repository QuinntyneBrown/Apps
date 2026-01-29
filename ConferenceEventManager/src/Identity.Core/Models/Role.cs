namespace Identity.Core.Models;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? TenantId { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
