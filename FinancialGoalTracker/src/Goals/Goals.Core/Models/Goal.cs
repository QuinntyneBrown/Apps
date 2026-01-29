namespace Goals.Core.Models;

public class Goal
{
    public Guid GoalId { get; private set; }
    public Guid TenantId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal TargetAmount { get; private set; }
    public decimal CurrentAmount { get; private set; }
    public DateTime TargetDate { get; private set; }
    public GoalStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Goal() { }

    public Goal(Guid tenantId, string name, decimal targetAmount, DateTime targetDate, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (targetAmount <= 0)
            throw new ArgumentException("Target amount must be greater than zero.", nameof(targetAmount));

        GoalId = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
        Description = description;
        TargetAmount = targetAmount;
        CurrentAmount = 0;
        TargetDate = targetDate;
        Status = GoalStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? name = null, string? description = null, decimal? targetAmount = null, DateTime? targetDate = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            Name = name;
        }

        if (description != null)
            Description = description;

        if (targetAmount.HasValue)
        {
            if (targetAmount.Value <= 0)
                throw new ArgumentException("Target amount must be greater than zero.", nameof(targetAmount));
            TargetAmount = targetAmount.Value;
        }

        if (targetDate.HasValue)
            TargetDate = targetDate.Value;
    }

    public void AddContribution(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        CurrentAmount += amount;

        if (CurrentAmount >= TargetAmount)
            Status = GoalStatus.Completed;
    }

    public void SetStatus(GoalStatus status)
    {
        Status = status;
    }
}

public enum GoalStatus
{
    Active,
    Completed,
    Cancelled
}
