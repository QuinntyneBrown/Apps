namespace MovieTVShowWatchlist.Core;

public class ViewingAbandoned : DomainEvent
{
    public Guid ItemId { get; set; }
    public string ItemType { get; set; } = string.Empty;
    public DateTime AbandonDate { get; set; }
    public decimal ProgressPercent { get; set; }
    public string? AbandonReason { get; set; }
    public decimal? QualityRating { get; set; }
    public bool WouldRetry { get; set; }
}
