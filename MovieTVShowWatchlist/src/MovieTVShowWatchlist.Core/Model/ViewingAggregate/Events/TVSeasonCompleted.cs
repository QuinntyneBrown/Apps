namespace MovieTVShowWatchlist.Core;

public class TVSeasonCompleted : DomainEvent
{
    public Guid SeasonId { get; set; }
    public Guid ShowId { get; set; }
    public int SeasonNumber { get; set; }
    public DateTime CompletionDate { get; set; }
    public TimeSpan BingeDuration { get; set; }
    public int EpisodesWatched { get; set; }
    public decimal? SeasonRating { get; set; }
    public bool NextSeasonIntent { get; set; }
}
