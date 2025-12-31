namespace MovieTVShowWatchlist.Core;

public class SeasonRating
{
    public Guid SeasonRatingId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid RatingId { get; set; }
    public Guid TVShowId { get; set; }
    public int SeasonNumber { get; set; }
    public decimal RatingValue { get; set; }
    public DateTime CreatedAt { get; set; }

    public Rating Rating { get; set; } = null!;
}
