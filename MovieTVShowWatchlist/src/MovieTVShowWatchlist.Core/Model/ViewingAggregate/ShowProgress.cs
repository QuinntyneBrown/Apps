namespace MovieTVShowWatchlist.Core;

public class ShowProgress
{
    public Guid ShowProgressId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid TVShowId { get; set; }
    public int? LastWatchedSeason { get; set; }
    public int? LastWatchedEpisode { get; set; }
    public int TotalEpisodesWatched { get; set; }
    public int CompletedSeasons { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletionDate { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
    public TVShow TVShow { get; set; } = null!;
}
