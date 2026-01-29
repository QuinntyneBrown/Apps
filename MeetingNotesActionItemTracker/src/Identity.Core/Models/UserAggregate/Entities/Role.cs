namespace Identity.Core.Models.UserAggregate.Entities;

public class Role
{
    public Guid RoleId { get; private set; }
    public Guid TenantId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    private Role() { }

    public Role(Guid tenantId, string name)
    {
        RoleId = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
    }
}
