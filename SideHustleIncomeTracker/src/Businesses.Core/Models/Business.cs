namespace Businesses.Core.Models;

public class Business
{
    public Guid BusinessId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? Category { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Business() { }

    public Business(Guid tenantId, Guid userId, string name, string? description = null, string? category = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Business name cannot be empty.", nameof(name));

        BusinessId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        Description = description;
        Category = category;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? name = null, string? description = null, string? category = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Business name cannot be empty.", nameof(name));
            Name = name;
        }

        if (description != null) Description = description;
        if (category != null) Category = category;
        UpdatedAt = DateTime.UtcNow;
    }
}
