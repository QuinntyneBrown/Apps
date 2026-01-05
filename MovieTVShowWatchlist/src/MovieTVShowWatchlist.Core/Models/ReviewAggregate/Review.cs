namespace MovieTVShowWatchlist.Core;

public class Review
{
    public Guid ReviewId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public bool HasSpoilers { get; set; }
    public DateTime ReviewDate { get; set; }
    public bool WouldRecommend { get; set; }
    public string? TargetAudience { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public ICollection<ReviewTheme> Themes { get; set; } = new List<ReviewTheme>();
}
