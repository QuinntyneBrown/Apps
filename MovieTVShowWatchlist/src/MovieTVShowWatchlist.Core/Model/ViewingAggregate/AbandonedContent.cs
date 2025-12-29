namespace MovieTVShowWatchlist.Core;

public class AbandonedContent
{
    public Guid AbandonedContentId { get; set; }
    public Guid UserId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public DateTime AbandonDate { get; set; }
    public decimal? ProgressPercent { get; set; }
    public string? AbandonReason { get; set; }
    public decimal? QualityRating { get; set; }
    public bool? WouldRetry { get; set; }
    public DateTime CreatedAt { get; set; }

    public User User { get; set; } = null!;
}
