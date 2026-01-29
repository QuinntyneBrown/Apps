namespace Contributions.Core.Models;
public class Contribution
{
    public Guid Id { get; set; }
    public Guid PlanId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ContributionDate { get; set; }
    public string? Source { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid? TenantId { get; set; }
}
