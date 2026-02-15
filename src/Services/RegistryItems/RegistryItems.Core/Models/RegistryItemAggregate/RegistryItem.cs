namespace RegistryItems.Core.Models;

public class RegistryItem
{
    public Guid RegistryItemId { get; set; }
    public Guid RegistryId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int QuantityRequested { get; set; } = 1;
    public int QuantityReceived { get; set; }
    public string? Url { get; set; }
    public string? ImageUrl { get; set; }
    public int Priority { get; set; } = 1;
    public bool IsReserved { get; set; }
    public Guid? ReservedByUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
