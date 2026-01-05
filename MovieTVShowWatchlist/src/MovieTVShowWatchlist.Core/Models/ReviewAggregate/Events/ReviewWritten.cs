namespace MovieTVShowWatchlist.Core;

public class ReviewWritten : DomainEvent
{
    public Guid ReviewId { get; set; }
    public Guid ContentId { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string ReviewText { get; set; } = string.Empty;
    public bool HasSpoilers { get; set; }
    public DateTime ReviewDate { get; set; }
    public List<string> ThemesDiscussed { get; set; } = new();
    public bool WouldRecommend { get; set; }
    public string? TargetAudience { get; set; }
}
