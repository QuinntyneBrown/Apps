namespace Contributions.Core.Models;

public class Contribution
{
    public Guid ContributionId { get; set; }
    public Guid RegistryItemId { get; set; }
    public Guid ContributorUserId { get; set; }
    public Guid TenantId { get; set; }
    public decimal Amount { get; set; }
    public string? Message { get; set; }
    public bool IsAnonymous { get; set; }
    public DateTime ContributedAt { get; set; } = DateTime.UtcNow;
}
