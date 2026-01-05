namespace MovieTVShowWatchlist.Core;

public class TVShowAddedToWatchlist : DomainEvent
{
    public Guid ShowId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PremiereYear { get; set; }
    public List<string> Genres { get; set; } = new();
    public int? NumberOfSeasons { get; set; }
    public string? Status { get; set; }
    public DateTime AddedDate { get; set; }
    public string? Priority { get; set; }
    public string? StreamingPlatform { get; set; }
}
