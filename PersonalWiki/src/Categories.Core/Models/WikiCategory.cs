namespace Categories.Core.Models;

public class WikiCategory
{
    public Guid WikiCategoryId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Guid? ParentCategoryId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private WikiCategory() { }

    public WikiCategory(Guid tenantId, Guid userId, string name, string? description = null, Guid? parentCategoryId = null)
    {
        WikiCategoryId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string? description = null, Guid? parentCategoryId = null)
    {
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        UpdatedAt = DateTime.UtcNow;
    }
}
