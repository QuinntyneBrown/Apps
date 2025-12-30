namespace MovieTVShowWatchlist.Core;

public class EpisodeViewingRecord
{
    public Guid EpisodeViewingRecordId { get; set; }
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public Guid EpisodeId { get; set; }
    public Guid TVShowId { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public DateTime WatchDate { get; set; }
    public string? Platform { get; set; }
    public Guid? BingeSessionId { get; set; }
    public int? ViewingDurationMinutes { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
}
