namespace MovieTVShowWatchlist.Core;

public class Episode
{
    public Guid EpisodeId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid TVShowId { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public string? Title { get; set; }
    public DateTime? AirDate { get; set; }
    public int? Runtime { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public TVShow TVShow { get; set; } = null!;
}
