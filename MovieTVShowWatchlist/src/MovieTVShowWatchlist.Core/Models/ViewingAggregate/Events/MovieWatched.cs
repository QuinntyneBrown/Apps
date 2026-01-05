namespace MovieTVShowWatchlist.Core;

public class MovieWatched : DomainEvent
{
    public Guid MovieId { get; set; }
    public DateTime WatchDate { get; set; }
    public string? ViewingLocation { get; set; }
    public string? ViewingPlatform { get; set; }
    public List<string> WatchedWith { get; set; } = new();
    public string? ViewingContext { get; set; }
    public bool IsRewatch { get; set; }
}
