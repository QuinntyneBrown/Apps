namespace GiftRegistryOrganizer.Core;

public class Registry
{
    public Guid RegistryId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public RegistryType Type { get; set; }
    public DateTime EventDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
