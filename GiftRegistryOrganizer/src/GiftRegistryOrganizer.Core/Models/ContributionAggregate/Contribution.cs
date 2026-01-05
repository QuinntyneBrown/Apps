namespace GiftRegistryOrganizer.Core;

public class Contribution
{
    public Guid ContributionId { get; set; }
    public Guid RegistryItemId { get; set; }
    public Guid TenantId { get; set; }
    public RegistryItem? RegistryItem { get; set; }
    public string ContributorName { get; set; } = string.Empty;
    public string? ContributorEmail { get; set; }
    public int Quantity { get; set; } = 1;
    public DateTime ContributedAt { get; set; } = DateTime.UtcNow;
}
