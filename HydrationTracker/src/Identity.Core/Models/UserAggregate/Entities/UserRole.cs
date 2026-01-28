namespace Identity.Core.Models.UserAggregate.Entities;

public class UserRole
{
    public Guid UserRoleId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    private UserRole() { }

    public UserRole(Guid tenantId, Guid userId, Guid roleId)
    {
        UserRoleId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        RoleId = roleId;
    }
}
