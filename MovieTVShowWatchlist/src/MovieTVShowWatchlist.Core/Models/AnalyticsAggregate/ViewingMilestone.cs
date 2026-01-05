namespace MovieTVShowWatchlist.Core;

public class ViewingMilestone
{
    public Guid ViewingMilestoneId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public MilestoneType MilestoneType { get; set; }
    public DateTime AchievementDate { get; set; }
    public int MetricAchieved { get; set; }
    public string? ContentBreakdown { get; set; }
    public CelebrationTier CelebrationTier { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
}
