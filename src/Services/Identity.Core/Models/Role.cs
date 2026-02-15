namespace Identity.Core.Models;

public class Role
{
    public Guid RoleId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    private Role() { }

    public static Role Create(string name, string? description = null)
    {
        return new Role
        {
            RoleId = Guid.NewGuid(),
            Name = name,
            Description = description
        };
    }
}
