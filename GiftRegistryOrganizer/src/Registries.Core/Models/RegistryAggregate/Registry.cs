namespace Registries.Core.Models;

public class Registry
{
    public Guid RegistryId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string EventType { get; set; } = string.Empty;
    public DateTime? EventDate { get; set; }
    public bool IsPublic { get; set; }
    public string? ShareCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
