namespace FollowUps.Core.Models;

public class FollowUp
{
    public Guid FollowUpId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private FollowUp() { }

    public FollowUp(Guid tenantId, Guid userId, string name)
    {
        FollowUpId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}
