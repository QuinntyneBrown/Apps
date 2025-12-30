namespace MovieTVShowWatchlist.Core;

public class WatchlistItem
{
    public Guid WatchlistItemId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime AddedDate { get; set; }
    public PriorityLevel? PriorityLevel { get; set; }
    public int? PriorityRank { get; set; }
    public string? RecommendationSource { get; set; }
    public string? MoodCategory { get; set; }
    public int? WatchOrderPreference { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public RemovalReason? RemovalReason { get; set; }

    public User User { get; set; } = null!;
}
