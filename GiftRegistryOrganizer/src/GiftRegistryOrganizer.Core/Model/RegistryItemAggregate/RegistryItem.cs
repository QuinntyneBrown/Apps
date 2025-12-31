namespace GiftRegistryOrganizer.Core;

public class RegistryItem
{
    public Guid RegistryItemId { get; set; }
    public Guid RegistryId { get; set; }
    public Guid TenantId { get; set; }
    public Registry? Registry { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? Url { get; set; }
    public int QuantityDesired { get; set; } = 1;
    public int QuantityReceived { get; set; } = 0;
    public Priority Priority { get; set; }
    public bool IsFulfilled { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
