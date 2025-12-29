namespace MovieTVShowWatchlist.Core;

public class TVShowCompleted : DomainEvent
{
    public Guid ShowId { get; set; }
    public DateTime CompletionDate { get; set; }
    public int TotalEpisodes { get; set; }
    public TimeSpan TotalViewingTime { get; set; }
    public decimal? OverallRating { get; set; }
    public bool RewatchInterest { get; set; }
    public string? SeriesFinaleReaction { get; set; }
}
