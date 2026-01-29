namespace Milestones.Core.Models;

public class Milestone
{
    public Guid MilestoneId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public int TargetCount { get; private set; }
    public int CurrentCount { get; private set; }
    public bool IsAchieved { get; private set; }
    public DateTime? AchievedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Milestone() { }

    public Milestone(Guid tenantId, Guid userId, string name, int targetCount, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (targetCount <= 0)
            throw new ArgumentException("Target count must be positive.", nameof(targetCount));

        MilestoneId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        TargetCount = targetCount;
        Description = description;
        CurrentCount = 0;
        IsAchieved = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string? name = null, string? description = null, int? targetCount = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            Name = name;
        }

        if (description != null)
            Description = description;

        if (targetCount.HasValue)
        {
            if (targetCount.Value <= 0)
                throw new ArgumentException("Target count must be positive.", nameof(targetCount));
            TargetCount = targetCount.Value;
            CheckAchievement();
        }
    }

    public void IncrementProgress(int amount = 1)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.", nameof(amount));

        CurrentCount += amount;
        CheckAchievement();
    }

    private void CheckAchievement()
    {
        if (!IsAchieved && CurrentCount >= TargetCount)
        {
            IsAchieved = true;
            AchievedAt = DateTime.UtcNow;
        }
    }
}
