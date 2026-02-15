namespace Milestones.Core.Models;

public class Milestone
{
    public Guid MilestoneId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid GoalId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal TargetAmount { get; private set; }
    public bool IsReached { get; private set; }
    public DateTime? ReachedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Milestone() { }

    public Milestone(Guid tenantId, Guid goalId, string name, decimal targetAmount)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (targetAmount <= 0)
            throw new ArgumentException("Target amount must be greater than zero.", nameof(targetAmount));

        MilestoneId = Guid.NewGuid();
        TenantId = tenantId;
        GoalId = goalId;
        Name = name;
        TargetAmount = targetAmount;
        IsReached = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? name = null, decimal? targetAmount = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            Name = name;
        }

        if (targetAmount.HasValue)
        {
            if (targetAmount.Value <= 0)
                throw new ArgumentException("Target amount must be greater than zero.", nameof(targetAmount));
            TargetAmount = targetAmount.Value;
        }
    }

    public void MarkAsReached()
    {
        if (!IsReached)
        {
            IsReached = true;
            ReachedAt = DateTime.UtcNow;
        }
    }
}
