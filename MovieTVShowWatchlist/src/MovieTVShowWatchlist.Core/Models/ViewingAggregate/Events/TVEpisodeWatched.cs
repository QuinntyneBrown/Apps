namespace MovieTVShowWatchlist.Core;

public class TVEpisodeWatched : DomainEvent
{
    public Guid EpisodeId { get; set; }
    public Guid ShowId { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public DateTime WatchDate { get; set; }
    public string? Platform { get; set; }
    public Guid? BingeSessionId { get; set; }
    public TimeSpan ViewingDuration { get; set; }
}
