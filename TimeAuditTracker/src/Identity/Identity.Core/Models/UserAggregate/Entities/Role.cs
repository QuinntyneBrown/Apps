namespace Identity.Core.Models.UserAggregate.Entities;

public class Role
{
    public Guid RoleId { get; private set; }
    public Guid TenantId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    private Role() { }

    public Role(Guid tenantId, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.", nameof(name));

        RoleId = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
        Description = description;
    }

    public void Update(string? name = null, string? description = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Role name cannot be empty.", nameof(name));
            Name = name;
        }

        if (description != null)
        {
            Description = description;
        }
    }
}
