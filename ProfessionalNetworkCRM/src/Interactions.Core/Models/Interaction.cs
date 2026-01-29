namespace Interactions.Core.Models;

public class Interaction
{
    public Guid InteractionId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Interaction() { }

    public Interaction(Guid tenantId, Guid userId, string name)
    {
        InteractionId = Guid.NewGuid();
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
