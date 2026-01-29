namespace Contributions.Core.Models;

public class Contribution
{
    public Guid ContributionId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid GoalId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime ContributionDate { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Contribution() { }

    public Contribution(Guid tenantId, Guid goalId, decimal amount, DateTime contributionDate, string? notes = null)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        ContributionId = Guid.NewGuid();
        TenantId = tenantId;
        GoalId = goalId;
        Amount = amount;
        ContributionDate = contributionDate;
        Notes = notes;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(decimal? amount = null, DateTime? contributionDate = null, string? notes = null)
    {
        if (amount.HasValue)
        {
            if (amount.Value <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            Amount = amount.Value;
        }

        if (contributionDate.HasValue)
            ContributionDate = contributionDate.Value;

        if (notes != null)
            Notes = notes;
    }
}
