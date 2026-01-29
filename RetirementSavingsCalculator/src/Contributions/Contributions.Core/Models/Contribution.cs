namespace Contributions.Core.Models;

public class Contribution
{
    public Guid ContributionId { get; set; }
    public Guid TenantId { get; set; }
    public Guid RetirementScenarioId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ContributionDate { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public bool IsEmployerMatch { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
