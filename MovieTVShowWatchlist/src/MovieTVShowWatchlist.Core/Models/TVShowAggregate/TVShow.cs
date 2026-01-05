namespace MovieTVShowWatchlist.Core;

public class TVShow
{
    public Guid TVShowId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PremiereYear { get; set; }
    public int? NumberOfSeasons { get; set; }
    public ShowStatus Status { get; set; }
    public string? ExternalId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    public ICollection<ContentGenre> Genres { get; set; } = new List<ContentGenre>();
    public ICollection<ContentAvailability> Availabilities { get; set; } = new List<ContentAvailability>();
}
