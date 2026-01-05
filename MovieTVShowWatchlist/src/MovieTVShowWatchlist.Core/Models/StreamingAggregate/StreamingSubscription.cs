namespace MovieTVShowWatchlist.Core;

public class StreamingSubscription
{
    public Guid StreamingSubscriptionId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string PlatformName { get; set; } = string.Empty;
    public SubscriptionStatus Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? SubscriptionTier { get; set; }
    public decimal? MonthlyCost { get; set; }
    public int ContentAccessCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}
