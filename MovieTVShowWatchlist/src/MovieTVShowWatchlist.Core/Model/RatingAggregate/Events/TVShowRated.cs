namespace MovieTVShowWatchlist.Core;

public class TVShowRated : DomainEvent
{
    public Guid RatingId { get; set; }
    public Guid ShowId { get; set; }
    public decimal RatingValue { get; set; }
    public DateTime RatingDate { get; set; }
    public int SeasonsWatched { get; set; }
    public bool IsCompleted { get; set; }
    public Dictionary<int, decimal> RatingEvolutionBySeason { get; set; } = new();
}
