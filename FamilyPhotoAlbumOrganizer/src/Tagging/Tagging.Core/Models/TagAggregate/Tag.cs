namespace Tagging.Core.Models;

public class Tag
{
    public Guid TagId { get; private set; }
    public Guid TenantId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Color { get; private set; }

    private Tag() { }

    public Tag(Guid tenantId, string name, string? color = null)
    {
        TagId = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
        Color = color;
    }

    public void Update(string? name = null, string? color = null)
    {
        if (name != null) Name = name;
        Color = color ?? Color;
    }
}
