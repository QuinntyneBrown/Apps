namespace MovieTVShowWatchlist.Core;

public class MovieRated : DomainEvent
{
    public Guid RatingId { get; set; }
    public Guid MovieId { get; set; }
    public decimal RatingValue { get; set; }
    public string RatingScale { get; set; } = string.Empty;
    public DateTime RatingDate { get; set; }
    public DateTime? ViewingDate { get; set; }
    public bool IsRewatchRating { get; set; }
    public string? Mood { get; set; }
}
