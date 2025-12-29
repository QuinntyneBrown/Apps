namespace MovieTVShowWatchlist.Core;

public class MovieAddedToWatchlist : DomainEvent
{
    public Guid MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public List<string> Genres { get; set; } = new();
    public string? Director { get; set; }
    public int? Runtime { get; set; }
    public DateTime AddedDate { get; set; }
    public string? PriorityLevel { get; set; }
    public string? RecommendationSource { get; set; }
    public string? Availability { get; set; }
}
